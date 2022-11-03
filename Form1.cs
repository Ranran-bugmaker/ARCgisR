using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10._12.arcgis1
{
    public partial class Form1 : Form
    {

        public static IGeometry geometry { get; private set; }

        public ILayer pCurLayar { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            axTOCControl1.SetBuddyControl(axMapControlmain);

            axMapControleye.ClearLayers();
            for (int i = 0; i < axMapControlmain.LayerCount; i++)
            {
                axMapControleye.AddLayer(axMapControlmain.get_Layer(i));
            }
            axMapControleye.Extent = axMapControlmain.FullExtent;
            axMapControleye.Refresh();
        }

        private void 打开mxd文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult a;

            ofd.Title = "打开mxd文件";
            //ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = "mxd|*.mxd";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.RestoreDirectory = false;
            a = ofd.ShowDialog();
            if (a != DialogResult.OK)
                return;
            axMapControlmain.LoadMxFile(ofd.FileName);
        }

        private void axMapControlmain_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            axMapControleye.ClearLayers();
            for (int i = 0; i < axMapControlmain.LayerCount; i++)
            {
                axMapControleye.AddLayer(axMapControlmain.get_Layer(i));
            }
            axMapControleye.Extent = axMapControlmain.FullExtent;
            axMapControleye.Refresh();
        }



        private void axMapControleye_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button == 1)//左键点击移动，俩个视图联动
            {
                IPoint pPoint = new PointClass();
                pPoint.PutCoords(e.mapX, e.mapY);
                axMapControlmain.CenterAt(pPoint);
                axMapControlmain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }

        private void axMapControlmain_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            //获得当前地图视图的外包矩形
            IEnvelope pEnvelope = (IEnvelope)e.newEnvelope;
            //获得GraphicsContainer对象
            IGraphicsContainer pGraphicsContainer = axMapControleye.Map as IGraphicsContainer;

            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            //清除对象中的所有图形元素
            pGraphicsContainer.DeleteAllElements();

            //获得矩形图形元素
            IRectangleElement pRectangleEle = new RectangleElementClass();
            IElement pElement = pRectangleEle as IElement;
            pElement.Geometry = pEnvelope;

            //设置FillShapeElement对象的symbol
            IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
            pFillShapeEle.Symbol = getFillSymbol();
            //进行填充
            pGraphicsContainer.AddElement((IElement)pFillShapeEle, 0);
            //刷新视图
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        /// <summary>
        /// 获得鹰眼视图显示方框的symbol
        /// </summary>
        /// <returns></returns>
        private IFillSymbol getFillSymbol()
        {
            //矩形框的边界线颜色
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 255;
            //边界线
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 2;
            pOutline.Color = pColor;
            //symbol的背景色
            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 0;
            //获得显示的图形元素
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            return pFillSymbol;
        }

        private void axMapControleye_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(e.mapX, e.mapY);
            axMapControlmain.CenterAt(pPoint);
            axMapControlmain.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void axMapControlmain_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            toolStripStatusLabel1.Text = e.mapX.ToString(".###")+@"  "+e.mapY.ToString(".###");

            if (axMapControlmain.LayerCount <= 0  || pCurLayar == null)
            {
                return;
            }

            //IPoint pt = axMapControlmain.ToMapPoint(e.x, e.y);
            //ILayer pLyr;
            ////对图层第三个进行显示
            //IFeatureLayer pFeatLyr = axMapControlmain.get_Layer(2) as IFeatureLayer;
            //pLyr = pFeatLyr as ILayer;
            //pFeatLyr.DisplayField = "FID";
            //string pTip;
            //pTip = pLyr.get_TipText(pt.X, pt.Y, 1);
            //pLyr.ShowTips = true;
            //axMapControlmain.ShowMapTips = true;


            //ShowTips显示的要素类
            //IFeatureLayer pFeatureLayer = pCurLayar as IFeatureLayer;
            //pFeatureLayer.DisplayField = "FID";    //ShowTips显示的字段名称
            //将两个ShowTips属性设置为true
            //pFeatureLayer.ShowTips = true;
            //axMapControlmain.ShowMapTips = true;

        }

        private void axMapControlmain_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            switch (Form2.Selectedindex)
            {
                case 0:
                    geometry = axMapControlmain.TrackRectangle();
                    break;
                case 1:
                    geometry = axMapControlmain.TrackCircle();
                    break;
                case 2:
                    geometry = axMapControlmain.TrackPolygon();
                    break;
                default:
                    break; 
            }
            
//            axMapControlmain.Map.SelectByShape(geometry, null, false);
//            //axMapControlmain.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);


//            // 获取选择集  
//            ISelection pSelection = axMapControlmain.Map.FeatureSelection;

//            // 打开属性标签  
//            //IEnumFeatureSetup pEnumFeatureSetup = pSelection as IEnumFeatureSetup;
//            //pEnumFeatureSetup.AllFields = true;
//            //// 获取要素  
//            IEnumFeature pEnumFeature = pSelection as IEnumFeature;
//            IFeature pFeature = pEnumFeature.Next();
//            IFeatureLayer[] players = new IFeatureLayer[axMapControlmain.LayerCount];

//            //FeatureLayer pFeatureLayer = new FeatureLayer();
//            //IFeatureLayer player = pFeatureLayer as IFeatureLayer;
//            //IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
//            //while (pFeature != null)
//            //{
//            //    pFeatureSelection.Add(pFeature);
//            //    pFeature = pEnumFeature.Next();
//            //}


//            IPoint pPoint;
//            IArray pArray;
//            IFeatureIdentifyObj pFeatIdObj;
//            IIdentifyObj pIdObj;
//            IIdentify pIdentify = (IIdentify)pCurLayar;
//            //pPoint = axMapControlmain.ToMapPoint(e.x, e.y);
//            pArray = pIdentify.Identify(geometry);
//            if (pArray != null)
//            { //获得FeatureIdentifyObj对象
//                //for (int i = 0; i < pArray.Count; i++)
//                //{
//                //    pFeatIdObj = (IFeatureIdentifyObj)pArray.get_Element(i);
//                //    pIdObj = pFeatIdObj as IIdentifyObj;
//                //    // 将被选择的要素闪烁
//                //    pIdObj.Flash(axMapControlmain.ActiveView.ScreenDisplay);
                    
//                //    //listBox1.Items.Add(pIdObj.Name);

//                //}
//                axMapControlmain.FlashShape();
//            }
//            else
//            {
//                MessageBox.Show("没有要素被点击");
//            }



//            //IMap pMap= axMapControlmain.Map;;
//            //IActiveView pActiveView = pMap as IActiveView;
//            //IPoint pt= axMapControlmain.ToMapPoint(e.x, e.y);
//            //IMarkerElement pMarkerElement= new MarkerElementClass();
//            //IElement pElement= pMarkerElement as IElement;
//            //pElement.Geometry = pt;
//            //IGraphicsContainer pGraphicsContainer= pMap as IGraphicsContainer;
//            //pGraphicsContainer.AddElement((IElement)pMarkerElement, 0);
//            //pActiveView.Refresh();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < axMapControlmain.LayerCount; i++)
            {
                axMapControlmain.get_Layer(i).Visible = true;
            }
            axMapControlmain.Refresh();
            axMapControleye.Refresh();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < axMapControlmain.LayerCount; i++)
            {
                axMapControlmain.get_Layer(i).Visible = false;
            }
            axMapControlmain.Refresh();
            axMapControleye.Refresh();
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button==1)
            {
                esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap basicMap = null;
                ILayer layer = null;
                object unk = null, data = null;
                axTOCControl1.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref unk, ref data);
                if (layer != null)
                {
                    pCurLayar = layer;
                }
            }
            if (e.button == 2)
            {
                esriTOCControlItem itemType=esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap basicMap=null;
                ILayer layer=null;
                object unk=null, data=null;
                axTOCControl1.HitTest(e.x, e.y, ref itemType,ref basicMap,ref layer,ref unk,ref data);
                if (layer !=null)
                {
                    pCurLayar = layer;
                    contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
                    pCurLayar.ShowTips = true;
                }
                else if (basicMap !=null)
                {
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
                

            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pCurLayar !=null)
            {
                pCurLayar.Visible = false;
                (axMapControlmain.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, pCurLayar, null);
            }
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pCurLayar != null)
            {
                pCurLayar.Visible = true;
                (axMapControlmain.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, pCurLayar, null);
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pCurLayar == null)
            {
                return;
            }
            axMapControlmain.ShowMapTips = true;
        }

        private void 关闭ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (pCurLayar == null)
            {
                return;
            }
            axMapControlmain.ShowMapTips = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.CurrentMap = axMapControlmain.Map;
            form2.Show();
        }




        /// <summary>
        /// 核心空间查询函数
        /// </summary>
        /// <param name="pFtClass">查询要素类</param>
        /// <param name="pWhereClause">SQL语句</param>
        /// <param name="pGeometry">空间查询范围</param>
        /// <param name="pSpRel">空间关系</param>
        /// <returns></returns>
        private DataTable SpatialSearch(IFeatureClass pFtClass, string pWhereClause, IGeometry pGeometry, esriSpatialRelEnum pSpRel)
        {
            //定义空间查询过滤器对象
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            //设置sql查询语句
            pSpatialFilter.WhereClause = pWhereClause;
            //设置查询范围
            pSpatialFilter.Geometry = pGeometry;
            //给定范围与查询对象的空间关系
            pSpatialFilter.SpatialRel = pSpRel;

            //查询结果以游标的形式返回(下面与属性查询一样)
            IFeatureCursor pFtCursor = pFtClass.Search(pSpatialFilter, false);
            IFeature pFt = pFtCursor.NextFeature();
            DataTable DT = new DataTable();
            //for (int i = 0; i < pFtCursor.Fields.FieldCount; i++)
            //{
            //    DataColumn dc = new DataColumn(pFtCursor.Fields.get_Field(i).Name,
            //        System.Type.GetType(ParseFieldType((pFtCursor.Fields.get_Field(i).Type))));
            //    DT.Columns.Add(dc);
            //}
            while (pFt != null)
            {
                DataRow dr = DT.NewRow();
                for (int i = 0; i < pFt.Fields.FieldCount; i++)
                {
                    dr[i] = pFt.get_Value(i);
                }
                DT.Rows.Add(dr);
                pFt = pFtCursor.NextFeature();
            }
            return DT;
        }
        //private static string ParseFieldType(esriFieldType FieldType)
        //{
        //    switch (FieldType)
        //    {
        //        case esriFieldType.esriFieldTypeInteger:
        //            return "System.Int32";
        //        case esriFieldType.esriFieldTypeOID:
        //            return "System.Int32";
        //        case esriFieldType.esriFieldTypeDouble:
        //            return "System.Double";
        //        case esriFieldType.esriFieldTypeDate:
        //            return "System.DateTime";
        //        default:
        //            return "System.String";
        //    }
        //}
    }
}
