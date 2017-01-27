﻿using com.wer.sc.data.store;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataUpdate_Code
    {
        private Plugin_DataProvider dataProvider;

        private CodeStore codeStore;

        private DataPathUtils utils;

        public DataUpdate_Code(Plugin_DataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
            this.utils = new DataPathUtils(dataProvider.GetDataPath());
            this.codeStore = new CodeStore(utils.GetCodePath());
        }

        public void Update()
        {
            List<CodeInfo> codes = dataProvider.GetCodes();
            codeStore.Save(codes);
        }
    }
}