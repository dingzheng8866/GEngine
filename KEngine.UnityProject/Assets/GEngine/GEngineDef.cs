﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GEngine
{
    public class GEngineDef
    {
        // 美术库用到
        public const string ResourcesBuildDir = "BundleResources";

        public const string AssetBundleExt = ".assetbundle";

        public const string AssetBundleBuildRelPath = "../Product/Bundles";
        public const string StreamingBundlesFolderName = "Bundles";

        public const string ProductRelPath = "../Product";

        /// <summary>
        /// 编辑状态的资源目录存放, Unity 5中采用自动对BundleResources整体打包，把编辑器部分移出
        /// </summary>
        //public const string ResourcesEditDir = "BundleEditing";

        // 打包缓存，一些不同步的资源，在打包时拷到这个目录，并进行打包
        public const string ResourcesBuildCacheDir = "_ResourcesCache_";

        public const string ResourcesBuildInfosDir = "ResourcesBuildInfos";

        public const string RedundaciesDir = "_Redundancies_";
    }
}
