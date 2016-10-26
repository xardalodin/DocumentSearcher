using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace FromSearchWindowsDocs.backend
{
    class SearchDocX
    {

        public static List<string> StringSplitComma(string tosplit)
        {
            return tosplit.Split(',').ToList();
        }

        public static bool DoesDocXContainTheseWords(string FileName,List<string> List)
        {
            try {
                Microsoft.Office.Interop.Word.Application a = new Microsoft.Office.Interop.Word.Application();
                object missing = System.Reflection.Missing.Value;

                a.Documents.Open(FileName);

                a.Selection.Find.ClearFormatting();

                foreach (var l in List)
                {
                    object ref_fix = l;  // cat pass a foreach l as ref , cant use for loop, must be object not var.. ARRGGHH
                    if (a.Selection.Find.Execute(ref ref_fix,
                      ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                      ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                      ref missing, ref missing))
                    {
                        // search term exists , do nothing
                    }
                    else
                    {
                        // search term does not exist in document
                        a.Quit(false);
                        GC.Collect();
                        return false;
                    }
                }
                a.Quit(false);
                GC.Collect();
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show("windows not installed of file not found or access to file not granted:{0}", er.ToString());
            }
             
          return true;
           
        }

    }
}
