using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Grasshopper.Kernel.Types;
using System.IO;
using System.Reflection;
using System.Linq;

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
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("assemblyName", "assemblyName", "assemblyName", GH_ParamAccess.list);
            pManager.AddTextParameter("version", "version", "version", GH_ParamAccess.list);
            pManager.AddTextParameter("assemblyName2", "assemblyName2", "version", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            testDto dto = new testDto()
            {
                name = "name",
                age = 3,
                height = 1.09,
                point = new GH_Point(new Point3d(0,0,0)),
            };

            List<string> allVersions = new List<string>();
            List<string> assemblyNames = new List<string>();
            string assemblyNames2 = "";
            string assemblyNames3 = "";
            string assemblyNames4 = "";
            string assemblyNames5 = "";

            string path = Environment.ExpandEnvironmentVariables("%appdata%/Grasshopper/Libraries/mergeDllsTest3.json");
            string dataJson = JsonConvert.SerializeObject(dto);
            File.WriteAllText(path, dataJson);

            List<AssemblyName> assembly = Assembly.GetExecutingAssembly().GetReferencedAssemblies().ToList();
            AssemblyName assembly2 = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            assemblyNames2 = assembly2.ToString();




            foreach (AssemblyName curName in assembly)
            {
                string info = curName.Version.ToString();
                allVersions.Add(info);
                assemblyNames.Add(curName.ToString());
            }

            DA.SetDataList(0, assemblyNames);
            DA.SetDataList(1, allVersions);
            DA.SetData(2, assemblyNames2);
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