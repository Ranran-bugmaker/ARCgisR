using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace _10._12.arcgis1
{
    public partial class Form2 : Form
    {
        private ISelection ifs;
        private static IGeometry seageometry { get; set; }
        private static int selectedindex =3;
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
                comboBox1.SelectedIndex = 3;
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
            wheretxt.Text += listBoxValues.SelectedItem.ToString();
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

        /// <summary>
        /// 根据已配置的查询条件来执行属性查询操作。
        /// </summary>
        private void SelectFeaturesByAttribute()
        {
            try
            {
                if (seageometry == null)
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
                            ifs = currentMap.FeatureSelection;
                            break;
                    }

                    //部分刷新操作，只刷新地理选择集的内容
                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);
                }
                else
                {
                    //定义空间查询过滤器对象
                    ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                    //设置sql查询语句
                    pSpatialFilter.WhereClause = wheretxt.Text;
                    //设置查询范围
                    pSpatialFilter.Geometry = seageometry;
                    //给定范围与查询对象的空间关系
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;

                    IFeatureSelection featureSelection = currentFeatureLayer as IFeatureSelection;
                    IActiveView activeView = currentMap as IActiveView;

                    //首先使用IMap接口的ClearSelection()方法清空地图选择集
                    currentMap.ClearSelection();
                    //根据定义的where语句使用IFeatureSelection接口的SelectFeatures方法选择要素，并将其添加到选择集中
                    featureSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                    ifs = currentMap.FeatureSelection;

                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);
                }
            }
            catch (Exception)
            {

                throw;
            }



        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (Form1.geometry == null)
            {
                return;
            }
            seageometry = Form1.geometry;
            comboBox1.SelectedIndex = 3;
            //Form1.
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1.fm1.TopMost = true;
            Form1.fm1.TopMost = false;
            selectedindex = comboBox1.SelectedIndex;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetElementDataTable(currentMap.FeatureSelection);
        }
        #region//获取对应要素的属性表dataTable
        private static DataTable GetElementDataTable(ISelection featureSelection)
        {
            ISelection selection = featureSelection;
            IEnumFeatureSetup iEnumFeatureSetup = (IEnumFeatureSetup)selection;
            iEnumFeatureSetup.AllFields = true;
            IEnumFeature pEnumFeature = (IEnumFeature)iEnumFeatureSetup;
            pEnumFeature.Reset();
            IFeature pFeature = pEnumFeature.Next();
            //
            DataTable table = new DataTable();
            IFields fields = pFeature.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                string FieldName = fields.get_Field(i).AliasName;
                table.Columns.Add(FieldName);
            }



            while (pFeature != null)
            {
                DataRow dataRow = table.NewRow();
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    string FieldValue = null;
                    FieldValue = Convert.ToString(pFeature.get_Value(i));
                    dataRow[i] = FieldValue;
                }
                table.Rows.Add(dataRow);
                pFeature = pEnumFeature.Next();

            }
            return table;

            //DataTable pdataTable = new DataTable();
            //IFeatureClass pFeatureclass = pFLayer.FeatureClass;
            ////获取图层属性目录
            //IFields pFields = pFeatureclass.Fields;
            //for (int i = 0; i < pFields.FieldCount; i++)
            //{
            //    string FieldName = pFields.get_Field(i).AliasName;
            //    pdataTable.Columns.Add(FieldName);
            //}
            ////游标
            //IFeatureCursor pFeatureCursor;
            //pFeatureCursor = pFeatureclass.Search(null, false);
            //IFeature pFeature;
            //pFeature = pFeatureCursor.NextFeature();
            //while (pFeature != null)
            //{
            //    DataRow row = pdataTable.NewRow();
            //    for (int i = 0; i < pFields.FieldCount; i++)
            //    {
            //        string FieldValue = null;
            //        FieldValue = Convert.ToString(pFeature.get_Value(i));
            //        row[i] = FieldValue;
            //    }
            //    pdataTable.Rows.Add(row);
            //    pFeature = pFeatureCursor.NextFeature();
            //}
            ////指针释放
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            //return pdataTable;
        }
        #endregion
    }
}
