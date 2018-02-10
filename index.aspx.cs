using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Windows;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void uploadWordListFile_Click(object sender, EventArgs e)
    {
        Label2.Text = "";
        Label3.Text = "";
        String a = "";
        // checks if the uploaded document is in text format
        String fileExtCheck = System.IO.Path.GetExtension(FileUpload2.FileName);

        if (fileExtCheck.ToLower() != ".txt")
        {
            Label1.Text = "Only Text Documents can be parsed";
        }
        else {
            FileUpload2.SaveAs(Server.MapPath("~/uploads/" + FileUpload2.FileName));
            Label1.Text = "Parse and compute successful";
        }

        //PHASE II get the list 
        //
        List<string> allLinesText = new List<string>();
        List<string> omitedLargestList = new List<string>(); //List where largest is removedas its not a superset

        FileUpload fu = FileUpload2;
        if (fu.HasFile)
        {
            StreamReader reader = new StreamReader(fu.FileContent);
            do
            {
               string textLine = reader.ReadLine();
               if(textLine != "")  allLinesText.Add(textLine);
            }
            while (reader.Peek() != -1);
            reader.Close();
        }

        //PHASE III CALCULATION
        //(allLinesText).Sort();
        allLinesText = allLinesText.OrderBy(s => s.Length).ToList();
        List<string> secondLongest = new List<string>(allLinesText);

    //find the longest line in the list
    here:string longestString = allLinesText.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);

        List<string> otherLongest = new List <string>();
        //PreviousPage 2nd
        //1st
        foreach (string s in allLinesText)
        {
            if (s.Length == longestString.Length ) otherLongest.Add(s.ToString());
        }

        foreach (var longest in otherLongest)
        {
            longestString = longest;
        
        List<string> Comblst = generateCombinations(longestString);
            Comblst = eliminateSubsetCombinations(Comblst, allLinesText);
        Comblst = eliminateNullSpacesFromList(Comblst);

        //remove the whole item itself
        try
        {
            if (Comblst.Count == 1) {
                if (Comblst[0] == longestString) {
                        //otherLongest.Remove(longestString);//then wrong longestString
                        //a = a + "," + longestString;
                        allLinesText.Remove(longestString);
                    }
            }
           Comblst.Remove(longestString);

        }catch (Exception ){}

            //now remove first occurance the longestString
            Comblst = Comblst.OrderByDescending(s => s.Length).ToList();
            foreach (var item in Comblst)
            {
                try
                {
                    longestString = longestString.Replace(item, "");
                }
                catch (Exception){}                
            }

            if (longestString.Length != 0)
            {
                //otherLongest.Remove(longest);
                a = a + "," + longest;
                allLinesText.Remove(longest);
            }
        }

        foreach (var item in a.Split(','))
        {
          otherLongest.Remove(item);
        }
        foreach (string item in otherLongest)
        {
            Label2.Text  =  Label2.Text + " " + item ;
        }

        if (Label2.Text == "") goto here;

        //2nd longest
        

        // secondLongest = allLinesText.CopyTo()

        //add second largest item
        var lenght = (allLinesText.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur)).Length;
        var temp = new List<string>(secondLongest);

        foreach (string s in temp)
        {
            if (s.Length >= lenght) secondLongest.Remove(s.ToString());
        }

        string secondLongString = secondLongest.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
        secondLongest.Clear();

        foreach (string s in temp)
        {
            if (s.Length == secondLongString.Length) secondLongest.Add(s.ToString());
        }
        //temp = null;

        foreach (var longest in secondLongest )
        {
            secondLongString  = longest;

            List<string> Comblst = generateCombinations(secondLongString);
            Comblst = eliminateSubsetCombinations(Comblst, allLinesText);
            Comblst = eliminateNullSpacesFromList(Comblst);

            //remove the whole item itself
            try
            {
                if (Comblst.Count == 1)
                {
                    if (Comblst[0] == secondLongString)
                    {
                        //otherLongest.Remove(longestString);//then wrong longestString
                        //a = a + "," + longestString;
                        allLinesText.Remove(secondLongString);
                    }
                }
                Comblst.Remove(secondLongString);

            }
            catch (Exception) { }

            //now remove first occurance the longestString
            Comblst = Comblst.OrderByDescending(s => s.Length).ToList();
            foreach (var item in Comblst)
            {
                try
                {
                    secondLongString = secondLongString.Replace(item, "");
                }
                catch (Exception) { }
            }

            if (secondLongString.Length != 0)
            {
                //otherLongest.Remove(longest);
                a = a + "," + longest;
                allLinesText.Remove(longest);
            }
        }

        foreach (var item in a.Split(','))
        {
            secondLongest.Remove(item);
        }
        foreach (string item in secondLongest)
        {
            Label3.Text = Label3.Text + " " + item;
        }

        string[] strArry = temp.ToArray();
        int k = permute(strArry, 0, 2);

        Label4.Text = Convert.ToString(Math.BigMul(temp.Count -1,temp.Count)); //k.ToString(); n(n-1) subsets
        k = 0;
    }



    List<string> eliminateNullSpacesFromList(List<string> Comblst)
    {
        return Comblst.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
    }

    static int k = 0;
    static int permute(string[] arry, int i, int n )
    {
        int j;
        k = k + 1;
        if (i == n)
            k = k + 1;
        else
        {
            for (j = i; j <= n; j++)
            {
                swap(ref arry[i], ref arry[j]);
                permute(arry, i + 1, n);
                swap(ref arry[i], ref arry[j]); //backtrack
            }
        }
        return k;
    }

    static void swap(ref string a, ref string b)
    {
        string tmp;
        tmp = a;
        a = b;
        b = tmp;
    }


    List<string> eliminateSubsetCombinations(List<string> Comblst, List<string> allLinesText) {
        //elimination of unnecessary subset Combinations
        for (int i = 0; i <= Comblst.Count - 1; i++)
        {
            if (!allLinesText.Contains(Comblst[i])) Comblst[i] = "";
        }
        return Comblst;
    }

    List<string> generateCombinations(String longestString)
    {
        List<string> lst = new List<string>();
        //generation of combinations

        //lst.Add("" + longestString[0]);
        for (int i = 0; i <= longestString.Length - 1; i++) {
            for (int j = 0; j <= longestString.Length - i; j++) {
                string str = longestString.Substring(i, j).ToString();
               if(str!="") lst.Add(str);
            }
        }
        return lst;
    }

}