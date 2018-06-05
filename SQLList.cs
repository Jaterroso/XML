using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Xml
{
    class SQLList
    {
        
        public static void TableToList(List<applicant> Ls, Table<applicant> table)
            {
                List<applicant> ls = Ls;
                ls.Clear();
                var result =
                from r in table
                orderby r.idapplicant
                select r;            
                foreach(var r in result)
                {
                applicant st = new applicant();
                st.FIO = r.FIO;
                st.hired = r.hired;
                st.idapplicant = r.idapplicant;
                st.Info = r.Info;
                st.position = r.position;
                st.salary = r.salary;
                ls.Add(st);
                }    
            }
        public static void TableToList(List<vacancies> Ls, Table<vacancies> table)
        {
            List<vacancies> ls = Ls;
            ls.Clear();
            var result =
            from r in table
            orderby r.Idvacant
            select r;
            foreach (var r in result)
            {
                vacancies st = new vacancies();
                st.Idvacant = r.Idvacant;
                st.dateopen = r.dateopen;
                st.dateclose = r.dateclose;
                st.position = r.position;
                st.salary = r.salary;
                ls.Add(st);
            }
        }
        public static void ListToXML(List<vacancies> L, string ExPath)
        {
            XmlDocument Xdoc = new XmlDocument();
            Xdoc.AppendChild(Xdoc.CreateProcessingInstruction("xml", "version='1.0' encoding='UTF-8'"));
            XmlElement Root = Xdoc.CreateElement("vacancies");
            Xdoc.AppendChild(Root);
            foreach (var l in L)
            {
                XmlElement NodeV = Xdoc.CreateElement("vacancies");
                NodeV.SetAttribute("Id", l.Idvacant.ToString());
                XmlElement fld = Xdoc.CreateElement("Position"); fld.InnerText = l.position;
                NodeV.AppendChild(fld);
                fld = Xdoc.CreateElement("Salary"); fld.InnerText = l.salary.ToString();
                NodeV.AppendChild(fld);
                fld = Xdoc.CreateElement("DateOpen"); fld.InnerText = l.dateopen.ToString();
                NodeV.AppendChild(fld);
                fld = Xdoc.CreateElement("DateClose"); fld.InnerText = l.dateclose.ToString();
                NodeV.AppendChild(fld);
                Root.AppendChild(NodeV);
            }
            Xdoc.Save(ExPath);
        }
        public static void ListToXML(List<applicant> L, string ExPath)
        {
            XmlDocument Xdoc = new XmlDocument();
            Xdoc.AppendChild(Xdoc.CreateProcessingInstruction("xml", "version='1.0' encoding='UTF-8'"));
            XmlElement Root = Xdoc.CreateElement("applicant");
            Xdoc.AppendChild(Root);
            foreach (var l in L)
            {
                XmlElement NodeV = Xdoc.CreateElement("vacancies");
                NodeV.SetAttribute("Id", l.idapplicant.ToString());
                XmlElement fld = Xdoc.CreateElement("FIO"); fld.InnerText = l.FIO.ToString();
                NodeV.AppendChild(fld);              
                fld = Xdoc.CreateElement("Position"); fld.InnerText = l.position;
                NodeV.AppendChild(fld);
                fld = Xdoc.CreateElement("Salary"); fld.InnerText = l.salary.ToString();
                NodeV.AppendChild(fld);
                fld = Xdoc.CreateElement("Hired"); fld.InnerText = l.hired.ToString();
                NodeV.AppendChild(fld);
                Root.AppendChild(NodeV);
            }
            Xdoc.Save(ExPath);
        }
    }
}
