﻿//Apache2, 2018, Apache/PdfBox Authors ( https://github.com/apache/pdfbox) 
//
//
//Apache PDFBox
//Copyright 2014 The Apache Software Foundation

//This product includes software developed at
//The Apache Software Foundation(http://www.apache.org/).

//Based on source code originally developed in the PDFBox and
//FontBox projects.

//Copyright (c) 2002-2007, www.pdfbox.org

//Based on source code originally developed in the PaDaF project.
//Copyright (c) 2010 Atos Worldline SAS

//Includes the Adobe Glyph List
//Copyright 1997, 1998, 2002, 2007, 2010 Adobe Systems Incorporated.

//Includes the Zapf Dingbats Glyph List
//Copyright 2002, 2010 Adobe Systems Incorporated.

//Includes OSXAdapter
//Copyright (C) 2003-2007 Apple, Inc., All Rights Reserved

//----------------
//ccf1 spec from http://wwwimages.adobe.com/www.adobe.com/content/dam/acom/en/devnet/font/pdfs/5176.CFF.pdf

//------------------------------------------------------------------
//Apache2, 2018, WinterDev


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Typography.OpenFont.CFF;

namespace Typography.OpenFont.Tables
{

    class CFFTable : TableEntry
    {

        public override string Name
        {
            get { return "CFF "; } //4 char, left 1 blank whitespace
        }

        protected override void ReadContentFrom(BinaryReader reader)
        {
            uint tableOffset = this.Header.Offset;
            if (reader.BaseStream.Position != tableOffset)
            {

            }

            //
            //
            //Table 8 Header Format
            //Type      Name    Description
            //Card8     major   Format major version(starting at 1)
            //Card8     minor   Format minor version(starting at 0)
            //Card8     hdrSize Header size(bytes)
            //OffSize   offSize Absolute offset(0) size
            byte[] header = reader.ReadBytes(4);

            byte major = header[0];
            byte minor = header[1];
            byte hdrSize = header[2];
            byte offSize = header[3];
            ////---------
            //name index

            switch (major)
            {
                default: throw new NotSupportedException();
                case 1:
                    {
                        Cff1Parser cff1 = new Cff1Parser();
                        cff1.ParseAfterHader(tableOffset, reader);
                    }
                    break;
                case 2:
                    {
                        Cff2Parser cff2 = new Cff2Parser();
                        cff2.ParseAfterHader(reader);
                    }
                    break;
            }
        }


        internal List<string> fontNames;
    }


}