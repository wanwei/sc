﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace com.wer.sc.plugin.cnfutures.market.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("com.wer.sc.plugin.cnfutures.market.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 A,豆一,DL
        ///B,豆二,DL
        ///BB,胶合板,DL
        ///C,玉米,DL
        ///CS,淀粉,DL
        ///FB,纤维板,DL
        ///I,铁矿石,DL
        ///J,焦炭,DL
        ///JD,鸡蛋,DL
        ///JM,焦煤,DL
        ///L,塑料,DL
        ///M,豆粕,DL
        ///P,棕榈,DL
        ///PP,聚丙烯,DL
        ///V,聚氯乙烯,DL
        ///Y,豆油,DL
        ///AU,黄金,SQ
        ///BU,沥青,SQ
        ///FU,燃油,SQ
        ///HC,轧卷板,SQ
        ///RB,螺纹钢,SQ
        ///RU,橡胶,SQ
        ///WR,线材,SQ
        ///AG,白银,SQ
        ///AL,沪铝,SQ
        ///CU,沪铜,SQ
        ///PB,沪铅,SQ
        ///ZN,沪锌,SQ
        ///NI,沪镍,SQ
        ///SN,沪锡,SQ
        ///CF,棉花,ZZ
        ///ER,籼稻,ZZ
        ///FG,玻璃,ZZ
        ///JR,粳稻,ZZ
        ///LR,晚籼稻,ZZ
        ///MA,甲醇,ZZ
        ///ME,甲醇,ZZ
        ///OI,新菜油,ZZ
        ///PM,普麦,ZZ
        ///RI,新籼稻,ZZ
        ///RM,菜粕,ZZ
        ///RO,(旧)菜油,ZZ
        ///RS,菜籽,ZZ
        ///SF,硅铁,ZZ
        ///SM,锰硅,ZZ
        ///SR,白糖,ZZ
        ///TA,PTA,ZZ
        ///TC,动力煤,ZZ
        ///WH,新强麦,ZZ
        ///WS,强麦,ZZ
        ///WT,硬麦 [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string catelogs {
            get {
                return ResourceManager.GetString("catelogs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;CONFIG&gt;
        ///  &lt;OPENTIMES DEFAULT=&quot;NORMAL&quot;&gt;
        ///    &lt;!--一般日盘时间 
        ///    大商所 M豆粕 Y豆油 A豆一 B豆二 P棕榈 J焦炭 JM焦煤 I铁矿
        ///    郑商所 SR白糖 CF棉花 PTA甲酸 ME甲醇 RM菜粕
        ///    --&gt;
        ///    &lt;OPENTIME ID=&quot;NORMAL&quot;&gt;
        ///      &lt;PERIOD START=&quot;0.09&quot; END=&quot;0.1015&quot; /&gt;
        ///      &lt;PERIOD START=&quot;0.103&quot; END=&quot;0.113&quot; /&gt;
        ///      &lt;PERIOD START=&quot;0.133&quot; END=&quot;0.15&quot; /&gt;
        ///    &lt;/OPENTIME&gt;
        ///    &lt;!--2004年11月30日前郑州开盘时间--&gt;
        ///    &lt;OPENTIME ID=&quot;ZZ_EARLY&quot;&gt;
        ///      &lt;PERIOD START=&quot;0.09&quot; END=&quot;0.113&quot; /&gt;
        ///      &lt;PERIOD START=&quot;0.133&quot; END=&quot;0.15&quot; /&gt;
        ///    &lt;/OPENTIME&gt;
        ///    &lt;!--2010年中前上期开盘时间--&gt;
        ///  [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string opentime {
            get {
                return ResourceManager.GetString("opentime", resourceCulture);
            }
        }
    }
}
