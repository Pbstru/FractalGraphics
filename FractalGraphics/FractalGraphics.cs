using ProjectPenguin.Cards;
using ProjectPenguin.Cards.ProcessorCardHelpers;
using System;
using NSGeo.Geometry;
using NSGeo.Geometry.Collections;
using System.Linq;
using System.Collections.Generic;
using NSGraphicManager;


namespace FractalGraphics
{
    public class FractalTree : ProcessorCardBase
    {
        public FractalTree() : base("分形树_支干", "分形树_支干", "", "娱乐卡组", "分形模拟")
        {

        }
        public override Guid CardGuid => Guid.Parse("{1AC99BB7-64B8-40A1-A0BE-1CE31FAB07C0}");

        public override void Build(IDataDelivery idd)
        {
            LineCurve line = new LineCurve();
            idd.GetDataItem(0, ref line);
            double ratio = 0.5;
            idd.GetDataItem(1, ref ratio);
            bool spacing = true;
            idd.GetDataItem(2, ref spacing);
            List<LineCurve> linelst = Fractal(line,ratio,spacing);
            idd.SetDataList(0, linelst);
        }

        protected override void AddInputSideDataCards(IDataCardEmbedmentHelper helper)
        {
            helper.AddWellKnownTypeOfDataCard(typeof(LineCurve), "原生图形", "原生图形", "", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne);
            helper.AddWellKnownTypeOfDataCard(typeof(double), "比例", "比例", "", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne,0.382);
            helper.AddWellKnownTypeOfDataCard(typeof(bool), "spacing", "spacing", "", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne, true);
        }

        protected override void AddOutputSideDataCards(IDataCardEmbedmentHelper helper)
        {
            helper.AddWellKnownTypeOfDataCard(typeof(LineCurve), "图形", "图形", "", ProjectPenguin.Cards.Enums.DataProcessingMode.ListByList);
        }

        public List<LineCurve> Fractal(LineCurve line,double ratio,bool spacing)
        {
            Random rd = new Random();
            //分支间的间隙
            double spacingVal;
            if (spacing)
            {
                spacingVal = rd.NextDouble() / 10;
            }
            else
            {
                spacingVal = 0;
            }
            List<LineCurve> linelst = new List<LineCurve>();
            //主干长度
            double length = line.PointAtStart.DistanceTo(line.PointAtEnd);
            //主干方向
            Vector3d v1_unit = (line.PointAtEnd - line.PointAtStart)/length;
            //枝干1的根部点
            Point3d joint1 = line.PointAtStart + v1_unit * length * ratio;
            //枝干2的根部点
            Point3d joint2 = line.PointAtStart + v1_unit * length * (ratio + spacingVal);

            //次主干
            LineCurve line1 = new LineCurve(joint1, line.PointAtEnd);
            //旋转角度，45~60度随机
            double angle1 = rd.Next(45, 60) * Math.PI / 180;
            v1_unit.Rotate(angle1, Vector3d.ZAxis);
            //生成枝干1
            Point3d pt1 = joint1 + v1_unit * length * ratio;
            LineCurve line2 = new LineCurve(joint1, pt1);
            //旋转角度，90~120度随机
            double angle2 = rd.Next(90, 120) * Math.PI / 180;
            v1_unit.Rotate(-angle2, Vector3d.ZAxis);
            //生成枝干2
            Point3d pt2 = joint2 + v1_unit * length * ratio;
            LineCurve line3 = new LineCurve(joint2, pt2);
            linelst.Add(line1);
            linelst.Add(line2);
            linelst.Add(line3);
            return linelst;
        }

    }
    public class FractalTree_2 : ProcessorCardBase
    {
        public FractalTree_2() : base("分形树_主干", "分形树_主干", "", "娱乐卡组", "分形模拟")
        {

        }
        public override Guid CardGuid => Guid.Parse("{934F449B-7092-4B81-89EC-B199F63B171B}");

        public override void Build(IDataDelivery idd)
        {
            LineCurve line = new LineCurve();
            idd.GetDataItem(0, ref line);
            double ratio = 0.5;
            idd.GetDataItem(1, ref ratio);
            bool spacing = true;
            idd.GetDataItem(2, ref spacing);
            List<LineCurve> linelst = Fractal(line, ratio, spacing);
            idd.SetDataList(0, linelst);
        }

        protected override void AddInputSideDataCards(IDataCardEmbedmentHelper helper)
        {
            helper.AddWellKnownTypeOfDataCard(typeof(LineCurve), "原生图形", "原生图形", "", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne);
            helper.AddWellKnownTypeOfDataCard(typeof(double), "比例", "比例", "", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne, 0.382);
            helper.AddWellKnownTypeOfDataCard(typeof(bool), "spacing", "spacing", "", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne, true);
        }

        protected override void AddOutputSideDataCards(IDataCardEmbedmentHelper helper)
        {
            helper.AddWellKnownTypeOfDataCard(typeof(LineCurve), "图形", "图形", "", ProjectPenguin.Cards.Enums.DataProcessingMode.ListByList);
        }

        public List<LineCurve> Fractal(LineCurve line, double ratio, bool spacing)
        {
            Random rd = new Random();
            //分支间的间隙
            double spacingVal;
            if (spacing)
            {
                spacingVal = rd.NextDouble() / 10;
            }
            else
            {
                spacingVal = 0;
            }
            List<LineCurve> linelst = new List<LineCurve>();
            //主干长度
            double length = line.PointAtStart.DistanceTo(line.PointAtEnd);
            //主干方向
            Vector3d v1_unit = (line.PointAtEnd - line.PointAtStart) / length;
            //枝干1的根部点
            Point3d joint1 = line.PointAtStart + v1_unit * length * ratio;
            //枝干2的根部点
            Point3d joint2 = line.PointAtStart + v1_unit * length * (ratio + spacingVal);
            //枝干3的根部点
            Point3d joint3 = line.PointAtStart + v1_unit * length * (1 - spacingVal);
            //枝干3的根部点
            Point3d joint4 = line.PointAtEnd;

            //旋转角度，45~60度随机
            double angle1 = rd.Next(45, 60) * Math.PI / 180;
            v1_unit.Rotate(angle1, Vector3d.ZAxis);
            //生成枝干1
            Point3d pt1 = joint1 + v1_unit * length * ratio;
            LineCurve line1 = new LineCurve(joint1, pt1);
            //旋转角度，90~120度随机
            double angle2 = rd.Next(90, 120) * Math.PI / 180;
            v1_unit.Rotate(-angle2, Vector3d.ZAxis);
            //生成枝干2
            Point3d pt2 = joint2 + v1_unit * length * ratio;
            LineCurve line2 = new LineCurve(joint2, pt2);

            //旋转角度，90~120度随机
            double angle3 = rd.Next(90, 120) * Math.PI / 180;
            v1_unit.Rotate(angle3, Vector3d.ZAxis);
            //生成枝干3
            Point3d pt3 = joint3 + v1_unit * length * ratio;
            LineCurve line3 = new LineCurve(joint3, pt3);
            //旋转角度，90~120度随机
            double angle4 = rd.Next(90, 120) * Math.PI / 180;
            v1_unit.Rotate(-angle4, Vector3d.ZAxis);
            //生成枝干4
            Point3d pt4 = joint4 + v1_unit * length * ratio;
            LineCurve line4 = new LineCurve(joint4, pt4);


            linelst.Add(line1);
            linelst.Add(line2);
            linelst.Add(line3);
            linelst.Add(line4);
            return linelst;
        }

    }
    public class Leaf: ProcessorCardBase
    {
        public Leaf() : base("分形树_树叶", "分形树_树叶", "", "娱乐卡组", "分形模拟")
        {

        }
        public override Guid CardGuid => Guid.Parse("{CD131479-605F-4DB9-A080-7B17072069DE}");

        public override void Build(IDataDelivery idd)
        {
            LineCurve lineLst = new LineCurve();
            idd.GetDataItem(0,ref lineLst);
            double ratio = 0.2;
            idd.GetDataItem(1, ref ratio);
            double lengthRatio = 0.2;
            double widthRatio = 0.08;
            idd.GetDataItem(2, ref lengthRatio);
            idd.GetDataItem(3, ref widthRatio);
            List<Curve> leafLst = DrawLeaf(lineLst, ratio,lengthRatio,widthRatio);


            idd.SetDataList(0, leafLst);
        }

        protected override void AddInputSideDataCards(IDataCardEmbedmentHelper helper)
        {
            helper.AddWellKnownTypeOfDataCard(typeof(LineCurve), "原生图形", "原生图形", "", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne);
            helper.AddWellKnownTypeOfDataCard(typeof(double), "比例", "比例", "第一片树叶距离枝干底部的比例", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne, 0.2);
            helper.AddWellKnownTypeOfDataCard(typeof(double), "叶片长度比例", "叶片长度比例", "树叶长度相对于枝干的比例", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne, 0.2);
            helper.AddWellKnownTypeOfDataCard(typeof(double), "叶片宽度比例", "叶片宽度比例", "树叶宽度相对于枝干的比例", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne, 0.08);
        }

        protected override void AddOutputSideDataCards(IDataCardEmbedmentHelper helper)
        {
            helper.AddWellKnownTypeOfDataCard(typeof(Curve), "图形", "图形", "", ProjectPenguin.Cards.Enums.DataProcessingMode.ListByList);
        }

        public List<Curve> DrawLeaf(LineCurve line, double ratio,double lengthRatio,double widthRatio)
        {
            Random rd = new Random();


            List<Curve> leaflst = new List<Curve>();
            //主干长度
            double length = line.PointAtStart.DistanceTo(line.PointAtEnd);
            //主干方向
            Vector3d v1_unit = (line.PointAtEnd - line.PointAtStart) / length;

            List<double> rdNums = GenerateRandomNumber(5, ratio);
            rdNums.Sort();
            for(int i = 0; i < rdNums.Count; i++)
            {
                double angle;
                Vector3d v1_unit_temp = (line.PointAtEnd - line.PointAtStart) / length;
                Point3d joint = line.PointAtStart + v1_unit * length * rdNums[i];
                if (i % 2 != 0)
                {
                    angle = rd.Next(45, 60) * Math.PI / 180;
                }
                else
                {
                    angle = -rd.Next(45, 60) * Math.PI / 180;
                }

                v1_unit_temp.Rotate(angle, Vector3d.ZAxis);

                //叶子的长度暂定为枝干长度的五分之一，宽度为枝干长度的2/15
                Point3d ptLeaf_up = joint + v1_unit_temp * length * lengthRatio;
                Point3d ptLeaf_center = joint + v1_unit_temp * length * lengthRatio / 2;
                v1_unit_temp.Rotate(Math.PI / 2, Vector3d.ZAxis);
                Point3d ptLeaf_left = ptLeaf_center + v1_unit_temp * length * widthRatio / 2;
                v1_unit_temp.Rotate(Math.PI, Vector3d.ZAxis);
                Point3d ptLeaf_right = ptLeaf_center + v1_unit_temp * length * widthRatio / 2;

                Arc arc1 = new Arc(joint, ptLeaf_left, ptLeaf_up);
                Arc arc2 = new Arc(joint, ptLeaf_right, ptLeaf_up);
                ArcCurve arcCurve1 = new ArcCurve(arc1);
                ArcCurve arcCurve2 = new ArcCurve(arc2);
                leaflst.Add(arcCurve1);
                leaflst.Add(arcCurve2);
            }
            return leaflst;
        }

        public List<double> GenerateRandomNumber(int number,double minVal)
        {
            Random rd = new Random();
            List<double> rdNums = new List<double>();
            int intminVal = (int)(minVal * 1000);
            for (int i = 0; i < number; i++)
            {
                double num = rd.Next(intminVal, 1000) / 1000.0;
                rdNums.Add(num);
            }
            return rdNums;    
        }


    }

    public class Paint : ProcessorCardBase
    {
        static NSRender nSRender = NSRender.CreateRender(0);
        List<IntPtr> intPtrs = new List<IntPtr>();
        public Paint() : base("Painting", "Painting", "", "娱乐卡组", "图形上色")
        {

        }
        public override Guid CardGuid => Guid.Parse("{F1EB402A-C7D5-4541-B7C9-7A20C75E37A4}");

        public override void Build(IDataDelivery idd)
        {
            List<NSGeo.Runtime.CommonObject> objs = new List<NSGeo.Runtime.CommonObject>();
            idd.GetDataList(0, objs);
            int red = 0, green = 0, blue = 0;
            idd.GetDataItem(1, ref red);
            idd.GetDataItem(2, ref green);
            idd.GetDataItem(3, ref blue);


            if (intPtrs.Count != 0)
            {
                foreach (IntPtr ip in intPtrs)
                {
                    nSRender.DeleteObject(ip);
                }
                intPtrs.Clear();
                
            }
            NSRenderCurveProperty renderProperty = new NSRenderCurveProperty();
            renderProperty.Color = System.Drawing.Color.FromArgb(red, green, blue);

            NSRenderMeshProperty renderMeshProperty = new NSRenderMeshProperty();
            renderMeshProperty.Color = System.Drawing.Color.FromArgb(red, green, blue);


            if (objs[0].GetType().ToString().Contains("Brep"))
            {
                for (int i = 0; i < objs.Count; i++)
                {

                    IntPtr intPtr = nSRender.RenderPlaneBrep((Brep)objs[i], renderMeshProperty);
                    intPtrs.Add(intPtr);

                }
            }
            else
            {
                for (int i = 0; i < objs.Count; i++)
                {
                    IntPtr intPtr = nSRender.RenderCurve(objs[i].ObjectPointer, 0.001, renderProperty);
                    intPtrs.Add(intPtr);
                }
            }
            idd.SetDataItem(0, nSRender);
            idd.SetDataList(1,intPtrs);

        }



        protected override void AddInputSideDataCards(IDataCardEmbedmentHelper helper)
        {
            helper.AddWellKnownTypeOfDataCard(typeof(NSGeo.Runtime.CommonObject), "图形", "图形", "", ProjectPenguin.Cards.Enums.DataProcessingMode.ListByList);
            helper.AddWellKnownTypeOfDataCard(typeof(int), "红", "红", "RGB值", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne,0);
            helper.AddWellKnownTypeOfDataCard(typeof(int), "绿", "绿", "RGB值", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne, 255);
            helper.AddWellKnownTypeOfDataCard(typeof(int), "蓝", "蓝", "RGB值", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne, 127);
        }

        protected override void AddOutputSideDataCards(IDataCardEmbedmentHelper helper)
        {
            helper.AddWellKnownTypeOfDataCard(typeof(NSRender), "图形渲染器", "图形渲染器", "", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne);
            helper.AddWellKnownTypeOfDataCard(typeof(IntPtr), "指针", "指针", "将每次运行后intPtrs里面的冗余指针删除", ProjectPenguin.Cards.Enums.DataProcessingMode.ListByList);
        }

    }
    public class PaintDelete : ProcessorCardBase
    {
        public PaintDelete() : base("PaintingDelete", "PaintingDelete", "", "娱乐卡组", "图形上色")
        {

        }
        public override Guid CardGuid => Guid.Parse("{4CC641B3-170D-47AE-81DB-9D779CF8DCA6}");

        public override void Build(IDataDelivery idd)
        {
            NSRender nSRender = NSRender.CreateRender(0);
            idd.GetDataItem(0, ref nSRender);
            List<IntPtr> intPtrs = new List<IntPtr>();
            idd.GetDataList(1, intPtrs);

            foreach (IntPtr ip in intPtrs)
            {
                nSRender.DeleteObject(ip);
            }
            intPtrs.Clear();
        }
        protected override void AddInputSideDataCards(IDataCardEmbedmentHelper helper)
        {
            helper.AddWellKnownTypeOfDataCard(typeof(NSRender), "图形渲染器", "图形渲染器", "", ProjectPenguin.Cards.Enums.DataProcessingMode.OneByOne);
            helper.AddWellKnownTypeOfDataCard(typeof(IntPtr), "指针", "指针", "", ProjectPenguin.Cards.Enums.DataProcessingMode.ListByList);
        }

        protected override void AddOutputSideDataCards(IDataCardEmbedmentHelper helper)
        {

        }

    }
}
