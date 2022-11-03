using System;
using _10._12.arcgis1;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;

namespace _10._12.arcgis1
{
    public partial class Form2 : Form
    {
        private static int selectedindex=-1;
        public static int Selectedindex { get { return selectedindex; } private set { selectedindex = value; } }
        public Form2()
        {
            InitializeComponent();
        }

        private IMap currentMap;    //当前MapControl控件中的Map对象
        private IFeatureLayer currentFeatureLayer;  //设置临时类变量来使用IFeatureLayer接口的当前图层对象
        private string currentFieldName;    //设置临时类变量来存储字段名称

        public IMap CurrentMap
        {
            set
            {
                currentMap = value;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                //将当前图层列表清空
                comboBoxLayerName.Items.Clear();

                string layerName;   //设置临时变量存储图层名称

                //对Map中的每个图层进行判断并加载名称
                for (int i = 0; i < currentMap.LayerCount; i++)
                {
                    //如果该图层为图层组类型，则分别对所包含的每个图层进行操作
                    if (currentMap.get_Layer(i) is GroupLayer)
                    {
                        //使用ICompositeLayer接口进行遍历操作
                        ICompositeLayer compositeLayer = currentMap.get_Layer(i) as ICompositeLayer;
                        for (int j = 0; j < compositeLayer.Count; j++)
                        {
                            //将图层的名称添加到comboBoxLayerName控件中
                            layerName = compositeLayer.get_Layer(j).Name;
                            comboBoxLayerName.Items.Add(layerName);
                        }
                    }
                    //如果图层不是图层组类型，则直接添加名称
                    else
                    {
                        layerName = currentMap.get_Layer(i).Name;
                        comboBoxLayerName.Items.Add(layerName);
                    }
                }

                //将comboBoxLayerName控件的默认选项设置为第一个图层的名称
                comboBoxLayerName.SelectedIndex = 0;
                //将comboBoxSelectMethod控件的默认选项设置为第一种选择方式
                comboBoxSelectMethod.SelectedIndex = 0;
            }
            catch { }
        }

        private void comboBoxLayerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //首先将字段列表和字段值列表清空
            listBoxFields.Items.Clear();
            listBoxValues.Items.Clear();

            IField field;   //设置临时变量存储使用IField接口的对象

            for (int i = 0; i < currentMap.LayerCount; i++)
            {
                if (currentMap.get_Layer(i) is GroupLayer)
                {
                    ICompositeLayer compositeLayer = currentMap.get_Layer(i) as ICompositeLayer;
                    for (int j = 0; j < compositeLayer.Count; j++)
                    {
                        //判断图层的名称是否与comboBoxLayerName控件中选择的图层名称相同
                        if (compositeLayer.get_Layer(j).Name == comboBoxLayerName.SelectedItem.ToString())
                        {
                            //如果相同则设置为整个窗体所使用的IFeatureLayer接口对象
                            currentFeatureLayer = compositeLayer.get_Layer(j) as IFeatureLayer;
                            break;
                        }
                    }
                }
                else
                {
                    //判断图层的名称是否与comboBoxLayerName中选择的图层名称相同
                    if (currentMap.get_Layer(i).Name == comboBoxLayerName.SelectedItem.ToString())
                    {
                        //如果相同则设置为整个窗体所使用的IFeatureLayer接口对象
                        currentFeatureLayer = currentMap.get_Layer(i) as IFeatureLayer;
                        break;
                    }
                }
            }

            //使用IFeatureClass接口对该图层的所有属性字段进行遍历，并填充listBoxFields控件
            for (int i = 0; i < currentFeatureLayer.FeatureClass.Fields.FieldCount; i++)
            {
                //根据索引值获取图层的字段
                field = currentFeatureLayer.FeatureClass.Fields.get_Field(i);
                //排除SHAPE字段，并在其它字段名称前后添加字符"和字符"
                if (field.Name.ToUpper() != "SHAPE")
                    listBoxFields.Items.Add(field.Name);
            }

            //更新labelwhere控件中的图层名称信息
            //labelwhere.Text = currentFeatureLayer.Name + " WHERE:";

            //将显示where语句的文本框内容清空
            wheretxt.Clear();
        }


        /// <summary>
        /// 根据已配置的查询条件来执行属性查询操作。
        /// </summary>
        private void SelectFeaturesByAttribute()
        {
            //使用FeatureLayer对象的IFeatureSelection接口来执行查询操作。这里有一个接口转换操作。
            IFeatureSelection featureSelection = currentFeatureLayer as IFeatureSelection;
            //新建IQueryFilter接口的对象来进行where语句的定义
            IQueryFilter queryFilter = new QueryFilterClass();
            //设置where语句内容
            queryFilter.WhereClause = wheretxt.Text;
            //通过接口转换使用Map对象的IActiveView接口来部分刷新地图窗口，从而高亮显示查询的结果
            IActiveView activeView = currentMap as IActiveView;

            //根据查询选择方式的不同，得到不同的选择集
            switch (comboBoxSelectMethod.SelectedIndex)
            {
                //在新建选择集的情况下
                case 0:
                    //首先使用IMap接口的ClearSelection()方法清空地图选择集
                    currentMap.ClearSelection();
                    //根据定义的where语句使用IFeatureSelection接口的SelectFeatures方法选择要素，并将其添加到选择集中
                    featureSelection.SelectFeatures(queryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                    break;
                //添加到当前选择集的情况
                case 1:
                    featureSelection.SelectFeatures(queryFilter, esriSelectionResultEnum.esriSelectionResultAdd, false);
                    break;
                //从当前选择集中删除的情况
                case 2:
                    featureSelection.SelectFeatures(queryFilter, esriSelectionResultEnum.esriSelectionResultXOR, false);
                    break;
                //从当前选择集中选择的情况
                case 3:
                    featureSelection.SelectFeatures(queryFilter, esriSelectionResultEnum.esriSelectionResultAnd, false);
                    break;
                //默认为新建选择集的情况
                default:
                    currentMap.ClearSelection();
                    featureSelection.SelectFeatures(queryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                    break;
            }

            //部分刷新操作，只刷新地理选择集的内容
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);
        }

        private void listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            //首先将listBoxValues控件中的字段属性值清空
            listBoxValues.Items.Clear();
            //将buttonGetUniqeValue按钮控件置为可用状态
            if (buttonGetUniqeValue.Enabled == false)
                buttonGetUniqeValue.Enabled = true;

            //设置整个窗体可用的字段名称

            if (listBoxFields.SelectedItem == null)
            {
                return;
            }
            else
            {
                string str = listBoxFields.SelectedItem.ToString();
                ////使用string类中的方法将字段名称中的两个"字符去掉
                //str = str.Substring(1);
                //str = str.Substring(0, str.Length - 1);
                currentFieldName = str;
            }
        }

        private void buttonGetUniqeValue_Click(object sender, EventArgs e)
        {

            try
            {
                //！！！！！！！！仅限于数据源来自于地理数据库
                ////使用FeatureClass对象的IDataset接口来获取dataset和workspace的信息
                //IDataset dataset = (IDataset)currentFeatureLayer.FeatureClass;
                ////使用IQueryDef接口的对象来定义和查询属性信息。通过IWorkspace接口的CreateQueryDef()方法创建该对象。
                //IQueryDef queryDef = ((IFeatureWorkspace)dataset.Workspace).CreateQueryDef();
                ////设置所需查询的表格名称为dataset的名称
                //queryDef.Tables = dataset.Name;
                ////设置查询的字段名称。可以联合使用SQL语言的关键字，如查询唯一值可以使用DISTINCT关键字。
                //queryDef.SubFields = "DISTINCT (" + currentFieldName + ")";     //这里特别要注意，IQueryDef接口只能用于查询ArcSDE、个人和文件地理数据库数据
                //                                                                //queryDef.WhereClause= dataset.Name+"="+ currentFieldName;


                ////使用IField接口获取当前所需要使用的字段的信息
                //IFields fields = currentFeatureLayer.FeatureClass.Fields;
                //IField field = fields.get_Field(fields.FindField(currentFieldName));


                ////执行查询并返回ICursor接口的对象来访问整个结果的集合
                //ICursor cursor = queryDef.Evaluate();


                ////对整个结果集合进行遍历，从而添加所有的唯一值
                ////使用IRow接口来操作结果集合。首先定位到第一个查询结果。
                //IRow row = cursor.NextRow();        //也可以使用IFeatureCursor进行遍历
                //                                    //如果查询结果非空，则一直进行添加操作
                //while (row != null)
                //{
                //    ;
                //    //对String类型的字段，唯一值的前后添加'和'，以符合SQL语句的要求
                //    if (field.Type == esriFieldType.esriFieldTypeString)
                //    {
                //        listBoxValues.Items.Add("\'" + row.Value[0].ToString() + "\'");
                //    }
                //    else
                //    {
                //        listBoxValues.Items.Add(row.Value[0].ToString());
                //    }
                //    //继续执行下一个结果的添加
                //    row = cursor.NextRow();
                //}



                // 属性过滤器
                IQueryFilter pQueryFilter = new QueryFilter();
                pQueryFilter.AddField(currentFieldName);

                // 要素游标
                IFeatureCursor pFeatureCursor = currentFeatureLayer.Search(pQueryFilter, true);
                ICursor pCursor = pFeatureCursor as ICursor;

                // 设置统计信息
                IDataStatistics pDataStatistics = new DataStatistics();
                pDataStatistics.Field = currentFieldName;
                pDataStatistics.Cursor = pCursor;

                // 获取唯一值
                IEnumerator uniqueValues = pDataStatistics.UniqueValues;
                uniqueValues.Reset();

                // 遍历唯一值
                List<string> list = new List<string>();
                while (uniqueValues.MoveNext())
                {
                    listBoxValues.Items.Add(uniqueValues.Current.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            wheretxt.Text += listBoxFields.SelectedItem.ToString();
        }

        private void listBoxValues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            wheretxt.Text+= listBoxValues.SelectedItem.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //执行属性查询操作，并关闭窗体
                SelectFeaturesByAttribute();
                //this.Close();
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //定义空间查询过滤器对象
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            //设置sql查询语句
            pSpatialFilter.WhereClause = wheretxt.Text;
            //设置查询范围
            if (Form1.geometry == null)
            {
                return;
            }
            pSpatialFilter.Geometry = Form1.geometry;
            //给定范围与查询对象的空间关系
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;

            IFeatureSelection featureSelection = currentFeatureLayer as IFeatureSelection;
            IActiveView activeView = currentMap as IActiveView;

            //首先使用IMap接口的ClearSelection()方法清空地图选择集
            currentMap.ClearSelection();
            //根据定义的where语句使用IFeatureSelection接口的SelectFeatures方法选择要素，并将其添加到选择集中
            featureSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedindex = comboBox1.SelectedIndex;
        }
    }
}
