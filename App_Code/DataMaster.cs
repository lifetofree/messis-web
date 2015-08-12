using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml.Serialization;

/// <summary>
/// Summary description for DataMaster
/// </summary>
[Serializable]
[XmlRoot("DataMaster")]
public class DataMaster
{
    [XmlElement("ReturnCode")]
    public string ReturnCode { get; set; }
    [XmlElement("ReturnIDX")]
    public int ReturnIDX { get; set; }

    [XmlElement("KindList")]
    public DetailKindList[] KindList { get; set; }

    [XmlElement("AssetList")]
    public DetailAssetList[] AssetList { get; set; }

    [XmlElement("SiteList")]
    public DetailSiteList[] SiteList { get; set; }

    [XmlElement("MaterialTypeList")]
    public DetailMaterialTypeList[] MaterialTypeList { get; set; }

    [XmlElement("UnitList")]
    public DetailUnitList[] UnitList { get; set; }

    [XmlElement("MaterialList")]
    public DetailMaterialList[] MaterialList { get; set; }

    [XmlElement("MaterialNameList")]
    public DetailMaterialNameList[] MaterialNameList { get; set; }

    [XmlElement("MaterialRegisterList")]
    public DetailMaterialRegisterList[] MaterialRegisterList { get; set; }

    [XmlElement("MaterialRecordList")]
    public DetailMaterialRecordList[] MaterialRecordList { get; set; }

    [XmlElement("MaterialRecordItemList")]
    public DetailMaterialRecordItemList[] MaterialRecordItemList { get; set; }
}

#region kind
[Serializable]
public class DetailKindList
{
    [XmlElement("KIDX")]
    public int KIDX { get; set; }
    [XmlElement("KName")]
    public string KName { get; set; }
    [XmlElement("KDesc")]
    public string KDesc { get; set; }
    [XmlElement("KStatus")]
    public int KStatus { get; set; }
}
#endregion kind

#region asset
[Serializable]
public class DetailAssetList
{
    [XmlElement("AsIDX")]
    public int AsIDX { get; set; }
    [XmlElement("AsName")]
    public string AsName { get; set; }
    [XmlElement("AsDesc")]
    public string AsDesc { get; set; }
    [XmlElement("AsStatus")]
    public int AsStatus { get; set; }
}
#endregion asset

#region material type
[Serializable]
public class DetailMaterialTypeList
{
    [XmlElement("TypeIDX")]
    public int TypeIDX { get; set; }
    [XmlElement("TypeName")]
    public string TypeName { get; set; }
    [XmlElement("TypeDesc")]
    public string TypeDesc { get; set; }
    [XmlElement("TypeStatus")]
    public int TypeStatus { get; set; }
}
#endregion material type

#region unit
[Serializable]
public class DetailUnitList
{
    [XmlElement("UnitIDX")]
    public int UnitIDX { get; set; }
    [XmlElement("UnitName")]
    public string UnitName { get; set; }
    [XmlElement("UnitDesc")]
    public string UnitDesc { get; set; }
    [XmlElement("UnitStatus")]
    public int UnitStatus { get; set; }
}
#endregion unit

#region site
[Serializable]
public class DetailSiteList
{
    [XmlElement("SiteIDX")]
    public int SiteIDX { get; set; }
    [XmlElement("SiteCode")]
    public string SiteCode { get; set; }
    [XmlElement("RSiteIDX")]
    public int RSiteIDX { get; set; }
    [XmlElement("ProjectName")]
    public string ProjectName { get; set; }
    [XmlElement("ManagerName")]
    public string ManagerName { get; set; }
    [XmlElement("StaffName")]
    public string StaffName { get; set; }
    [XmlElement("SiteLocation")]
    public string SiteLocation { get; set; }
    [XmlElement("SiteStart")]
    public string SiteStart { get; set; }
    [XmlElement("SiteEnd")]
    public string SiteEnd { get; set; }
    [XmlElement("SiteStatus")]
    public int SiteStatus { get; set; }

    [XmlElement("SiteCodeName")]
    public string SiteCodeName { get; set; }
}
#endregion site

#region material
[Serializable]
public class DetailMaterialList
{
    [XmlElement("MIDX")]
    public int MIDX { get; set; }
    [XmlElement("MCode")]
    public string MCode { get; set; }
    [XmlElement("RMIDX")]
    public int RMIDX { get; set; }
    [XmlElement("MNIDX")]
    public int MNIDX { get; set; }
    [XmlElement("MName")]
    public string MName { get; set; }
    [XmlElement("TypeIDX")]
    public int TypeIDX { get; set; }
    [XmlElement("TypeName")]
    public string TypeName { get; set; }
    [XmlElement("UnitIDX")]
    public int UnitIDX { get; set; }
    [XmlElement("UnitName")]
    public string UnitName { get; set; }
    [XmlElement("AsIDX")]
    public int AsIDX { get; set; }
    [XmlElement("AsName")]
    public string AsName { get; set; }
    [XmlElement("MDesc")]
    public string MDesc { get; set; }
    [XmlElement("KIDX")]
    public int KIDX { get; set; }
    [XmlElement("KName")]
    public string KName { get; set; }
    [XmlElement("RUD")]
    public float RUD { get; set; }
    [XmlElement("MStatus")]
    public int MStatus { get; set; }
}

[Serializable]
public class DetailMaterialNameList
{
    [XmlElement("MNIDX")]
    public int MNIDX { get; set; }
    [XmlElement("MName")]
    public string MName { get; set; }
    [XmlElement("MNStatus")]
    public int MNStatus { get; set; }
}

[Serializable]
public class DetailMaterialRegisterList
{
    [XmlElement("RegIDX")]
    public int RegIDX { get; set; }
    [XmlElement("MIDX")]
    public int MIDX { get; set; }

    [XmlElement("MCode")]
    public string MCode { get; set; }
    [XmlElement("MDesc")]
    public string MDesc { get; set; }

    [XmlElement("RCode")]
    public string RCode { get; set; }
    [XmlElement("MName")]
    public string MName { get; set; }
    [XmlElement("AsName")]
    public string AsName { get; set; }
    [XmlElement("KName")]
    public string KName { get; set; }
    [XmlElement("RUD")]
    public float RUD { get; set; }

    [XmlElement("SerialNo")]
    public string SerialNo { get; set; }
    [XmlElement("PurDate")]
    public string PurDate { get; set; }
    [XmlElement("PurFrom")]
    public string PurFrom { get; set; }
    [XmlElement("Quantity")]
    public int Quantity { get; set; }
    [XmlElement("VoucherNo")]
    public string VoucherNo { get; set; }
    [XmlElement("UnitPrice")]
    public float UnitPrice { get; set; }
    [XmlElement("Amount")]
    public float Amount { get; set; }
    [XmlElement("UseStatus")]
    public int UseStatus { get; set; }
    [XmlElement("RegStatus")]
    public int RegStatus { get; set; }

    [XmlElement("CMCode")]
    public string CMCode { get; set; }

    [XmlElement("MCount")]
    public string MCount { get; set; }
}

[Serializable]
public class DetailMaterialRecordList
{
    [XmlElement("RecIDX")]
    public int RecIDX { get; set; }
    [XmlElement("RecNo")]
    public string RecNo { get; set; }
    [XmlElement("RecDate")]
    public string RecDate { get; set; }
    [XmlElement("RecFromSite")]
    public int RecFromSite { get; set; }
    [XmlElement("RecFromSiteName")]
    public string RecFromSiteName { get; set; }
    [XmlElement("RecToSite")]
    public int RecToSite { get; set; }
    [XmlElement("RecToSiteName")]
    public string RecToSiteName { get; set; }
    [XmlElement("DeliveryDate")]
    public string DeliveryDate { get; set; }
    [XmlElement("SenderSign")]
    public string SenderSign { get; set; }
    [XmlElement("ReceiverSign")]
    public string ReceiverSign { get; set; }
    [XmlElement("TruckNo")]
    public int TruckNo { get; set; }
    [XmlElement("RecComment")]
    public string RecComment { get; set; }
    [XmlElement("RecCreate")]
    public string RecCreate { get; set; }
    [XmlElement("RecUpdate")]
    public string RecUpdate { get; set; }
    [XmlElement("RecStatus")]
    public int RecStatus { get; set; }
}

[Serializable]
public class DetailMaterialRecordItemList
{
    [XmlElement("RecMatIDX")]
    public int RecMatIDX { get; set; }
    [XmlElement("RecIDX")]
    public int RecIDX { get; set; }
    [XmlElement("RegIDX")]
    public int RegIDX { get; set; }
    [XmlElement("RecQty")]
    public int RecQty { get; set; }
    [XmlElement("RecRemark")]
    public string RecRemark { get; set; }
    [XmlElement("RecMatStatus")]
    public int RecMatStatus { get; set; }

    [XmlElement("CMCode")]
    public string CMCode { get; set; }
    [XmlElement("MName")]
    public string MName { get; set; }
    [XmlElement("MDesc")]
    public string MDesc { get; set; }
    [XmlElement("SerialNo")]
    public string SerialNo { get; set; }
}
#endregion
