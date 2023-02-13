using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Grasshopper.Kernel.Types;
using System.IO;

namespace MergeDllsIntoOne
{
    public class GHTestComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GHTestComponent class.
        /// </summary>
        public GHTestComponent()
          : base("GHTestComponent", "GHTestComponent",
              "GHTestComponent",
              "GHTestComponent", "GHTestComponent")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("name", "name", "name", GH_ParamAccess.item);
            pManager.AddIntegerParameter("age", "age", "age", GH_ParamAccess.item);
            pManager.AddNumberParameter("height", "height", "height", GH_ParamAccess.item);
            pManager.AddPointParameter("point", "point", "point", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("jsonOutput", "jsonOutput", "jsonOutput", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = null;
            int age = 0;
            double height = 0;
            GH_Point pt = null;

            if(!DA.GetData(0, ref name)) { return; }
            if (!DA.GetData(1, ref age)) { return; }
            if (!DA.GetData(2, ref height)) { return; }
            if (!DA.GetData(3, ref pt)) { return; }

            testDto dto = new testDto()
            {
                name = name,
                age = age,
                height = height,
                point = pt,
            };

            string path = Environment.ExpandEnvironmentVariables("%appdata%/Grasshopper/Libraries/mergeDllsTest.json");
            string dataJson = JsonConvert.SerializeObject(dto);
            File.WriteAllText(path, dataJson);

            DA.SetData(0, dataJson);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8e6f3ca9-491f-49b0-b1f3-0010c7f6ba4c"); }
        }
    }
}