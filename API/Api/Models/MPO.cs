using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class MPO
    {
        public string strDeComID { get; set; }

        public int intAdnin { get; set; }

        public string strUserID { get; set; }

        public string strCardNo { get; set; }

        public string strResponse { get; set; }

        public List<MPO> ogrp { get; set; }

        public string strPassWord { get; set; }

        public string strbranchID { get; set; }

        public string strRole { get; set; }

        public string strUserPassword { get; set; }

        public string strTeritorryCode { get; set; }

        public string strTeritorryName { get; set; }

        public string strLedgerName { get; set; }

        public string strMerzeName { get; set; }

        public short intMpoType { get; set; }

        public string strMobileNo { get; set; }

        public short lngUniqueNo { get; set; }

        public string strEMP_CARD_NO { get; set; }

        public string strTeam { get; set; }

        public string strZone { get; set; }

        public string strDivision { get; set; }

        public string strArea { get; set; }

        public string branchid { get; set; }
    }
}