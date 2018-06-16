using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class GUIDRefSprite
{
   



    [MenuItem("Assets/Sprite 冗余资源查询 ")]   // 菜单开启并点击的   处理
    public static void FindAll()
    {
        new FindSprite().Find();
    }

    public class Item
    {
        public string guid;
        public string path;
        public List<string> materials   = new List<string>();
        public List<string> others      = new List<string>();

        public override string ToString()
        {
            string materStr = "";
            string gap = "";
            for(int i = 0 ; i < materials.Count; i ++)
            {
                materStr += gap + materials[i];
                gap = "\n ";
            }

            string othersStr = "";
            gap = "";
            for(int i = 0 ; i < others.Count; i ++)
            {
                othersStr += gap + others[i];
                gap = "\n";
            }

            return string.Format("<tr> \n    <td>{0}</td>\n   \n    <td>{1}</td>  \n    <td><pre>{2}</pre></td>  \n    <td><pre>{3}</pre></td> \n</tr>", guid, path, materStr, othersStr);
        }
    }


    public class FindSprite
    {
        string[] guids;
        string[] files;
        int fileIndex = 0;
        int fileCount = 0;

        List<Item> list = new List<Item>();

        int itemCount = 0;

        bool isCancel = false;

        public void Find()
        {
            guids = AssetDatabase.FindAssets("t:Sprite");
            itemCount = guids.Length;

            if (itemCount == 0)
            {
                Debug.Log("No Sprite");
                return;
            }

            List<string> withoutExtensions = new List<string>() { ".prefab", ".unity", ".mat", ".asset" };

            files = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
                .Where(s => withoutExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();

            fileCount = files.Length;
            if (fileCount == 0)
            {
                Debug.Log("No File");
                return;
            }

            for(int i = 0; i < itemCount; i ++)
            {

                string path = AssetDatabase.GUIDToAssetPath(guids[i]);

                if (path.StartsWith("Assets/Game/Arts_Effect"))
                    continue;
                
                if (!path.StartsWith("Assets/_"))
                {
                    Item item = new Item();
                    item.guid = guids[i];
                    item.path = path;
                    list.Add(item);
                }
            }

            itemCount = list.Count;

            EditorApplication.update = OnUpdate;
        }

        void OnUpdate()
        {

            string file = files[fileIndex];
            string relativePath = GetRelativeAssetsPath(file);


            string content = File.ReadAllText(file);



            bool isMat = file.EndsWith(".mat");

            string title = "查找Sprite引用 " + fileIndex + "/" + fileCount;


            isCancel = EditorUtility.DisplayCancelableProgressBar(title, file, (float)fileIndex / (float)fileCount);

            for(int i = 0; i < list.Count; i ++)
            {
//                if (i % 100 == 0)
//                {
//                    isCancel = EditorUtility.DisplayCancelableProgressBar(title + "    " + i + "/" + itemCount, file, (float)i / (float)itemCount);
//                    if (isCancel)
//                        break;
//                }

                if (Regex.IsMatch(content, list[i].guid))
                {
                    if (isMat)
                    {
                        list[i].materials.Add(relativePath);
                    }
                    else
                    {
                        list[i].others.Add(relativePath);
                    }
                }
            }

            fileIndex++;

            if (isCancel || fileIndex >= fileCount)
            {
                OnAllEnd();
            }

        }


        private string GetRelativeAssetsPath(string path)
        {
            return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
        }
       

        public void OnAllEnd()
        {
            EditorApplication.update = null;
            EditorUtility.ClearProgressBar();

            StringWriter swlist_0 = new StringWriter();

            StringWriter sw_0 = new StringWriter();
            StringWriter sw_1 = new StringWriter();
            StringWriter sw_2 = new StringWriter();
            StringWriter sw_3 = new StringWriter();
            for(int i = 0; i < list.Count; i ++)
            {
                if (list[i].materials.Count > 0 && list[i].others.Count == 0)
                {
                    sw_0.WriteLine(list[i]);
                    swlist_0.WriteLine(list[i].path);
                }
                else if(list[i].materials.Count == 0 && list[i].others.Count > 0)
                {
                    sw_1.WriteLine(list[i]);
                }
                else if(list[i].materials.Count == 0 && list[i].others.Count == 0)
                {
                    sw_2.WriteLine(list[i]);
                }
                else
                {
                    sw_3.WriteLine(list[i]);
                }
            }

            for (int i = 0; i < 4; i++)
            {

                StringWriter sw = new StringWriter();

                string name = "";

                switch(i)
                {
                    case 0:
                        name = "Materials";
                        break;
                    case 1:
                        name = "Prefab Or Other";
                        break;
                    case 2:
                        name = "没有被引用";
                        break;
                    case 3:
                        name = "混合";
                        break;

                }



                sw.WriteLine(@"<html>
<head>
<meta charset='utf-8'>
<style type='text/css'>

table
{
    border-collapse: collapse;
    border-spacing: 0;
}
td, th
{
    padding: 0.5rem 1rem;
    border: 1px solid #e9ebec;
}

 th
{
    font-weight: bold;
}
</style>
<title>"+i +" " + name+@"</title>
</head>
<body>");



                sw.WriteLine("<h1>"+name+"</h1>");
                sw.WriteLine("<table>");
                sw.WriteLine("<tr> <th>guid</th>  <th>path</th>  <th>Materials</th>   <th>Prefabs Or Others</th> </tr>");



                switch(i)
                {
                    case 0:
                        sw.WriteLine(sw_0.ToString());
                        break;
                    case 1:
                        sw.WriteLine(sw_1.ToString());
                        break;
                    case 2:
                        sw.WriteLine(sw_2.ToString());
                        break;
                    case 3:
                        sw.WriteLine(sw_3.ToString());
                        break;

                }


                sw.WriteLine("</table>");

                sw.WriteLine("</body>\n</html>");

                string path = Application.dataPath + "/../sprite_guid_" + i + name+".html";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                File.WriteAllText(path, sw.ToString());

                Application.OpenURL("file:///" + path);


                path = Application.dataPath + "/../sprite_guid_list" + i + name+".txt";

                switch(i)
                {
                    case 0:
                        File.WriteAllText(path, sw_0.ToString());
                        break;
                    case 1:
                        File.WriteAllText(path, sw_1.ToString());
                        break;
                    case 2:
                        File.WriteAllText(path, sw_2.ToString());
                        break;
                    case 3:
                        File.WriteAllText(path, sw_3.ToString());
                        break;

                }

            }


            File.WriteAllText(Application.dataPath + "/../sprite_path_list_0 Materials.txt", swlist_0.ToString());
        }
    }



}
