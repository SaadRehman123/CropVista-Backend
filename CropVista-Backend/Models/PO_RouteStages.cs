namespace CropVista_Backend.Models
{
    public class PO_RouteStages
    {
        public string PO_productionOrderId { get; set; }
        public string PO_RouteStageId { get; set; }
        public int PO_RouteStage { get; set; }
        public string PO_Type { get; set; }
        public string PO_ItemNo { get; set; }
        public string PO_ItemDescription { get; set; }
        public int PO_Quantity { get; set; }
        public string PO_Uom { get; set; }
        public string PO_WarehouseId { get; set; }
        public double PO_UnitPrice { get; set; }
        public double PO_Total { get; set; }
        public string PO_Status { get; set; }
    }
}
