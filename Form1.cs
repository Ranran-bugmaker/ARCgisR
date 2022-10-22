using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
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
        public ILayer pCurLayar { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            axTOCControl1.SetBuddyControl(axMapControlmain);
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
        }

        private void axMapControlmain_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IMap pMap= axMapControlmain.Map;;
            IActiveView pActiveView = pMap as IActiveView;
            IPoint pt= axMapControlmain.ToMapPoint(e.x, e.y);
            IMarkerElement pMarkerElement= new MarkerElementClass();
            IElement pElement= pMarkerElement as IElement;
            pElement.Geometry = pt;
            IGraphicsContainer pGraphicsContainer= pMap as IGraphicsContainer;
            pGraphicsContainer.AddElement((IElement)pMarkerElement, 0);
            pActiveView.Refresh();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < axMapControlmain.LayerCount-1; i++)
            {
                axMapControlmain.get_Layer(i).Visible = true;
            }
            axMapControlmain.Refresh();
            axMapControleye.Refresh();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < axMapControlmain.LayerCount - 1; i++)
            {
                axMapControlmain.get_Layer(i).Visible = false;
            }
            axMapControlmain.Refresh();
            axMapControleye.Refresh();
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
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
    }
}
