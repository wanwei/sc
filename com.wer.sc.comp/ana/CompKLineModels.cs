﻿using com.wer.sc.ana;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.ana
{
    public class CompKLineModels
    {
        private List<KLineModel> models = new List<KLineModel>();

        private List<Type> modelTypes = new List<Type>();

        public CompKLineModels()
        {
            List<PluginInfo> plugins = PluginMgr.Instance.Load();

            for (int i = 0; i < plugins.Count; i++)
            {
                modelTypes.AddRange(plugins[i].KLineModels);
            }
            for (int i = 0; i < modelTypes.Count; i++)
            {
                models.Add((KLineModel)Activator.CreateInstance(modelTypes[i]));
            }
        }

        public void Bind(TreeView treeView, Type type)
        {
            for (int i = 0; i < models.Count; i++)
            {
                Type modelType = modelTypes[i];
                if (!modelType.IsDefined(type, false))
                    continue;
                var attributes = modelType.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute.GetType() == type)
                    {
                        String name = (String)attribute.GetType().GetProperty("Name").GetValue(attribute);
                        TreeNode node = treeView.Nodes.Add(name + "  " + modelType.Name.ToString());
                        node.Tag = modelType;
                    }
                }
            }
        }

        private TreeView treeView;

        public void Bind(TreeView treeView)
        {
            this.treeView = treeView;
            Bind(treeView, typeof(ZbRegisterAttribute));
        }

        public void UnBind()
        {
            this.treeView = null;
        }

        public KLineModel CreateModel(Type type)
        {
            return (KLineModel)Activator.CreateInstance(type);
        }

        public KLineModel CreateSelectModel()
        {
            TreeNode node = treeView.SelectedNode;
            if (node == null)
                return null;
            Type type = (Type)node.Tag;
            return CreateModel(type);
        }
    }
}