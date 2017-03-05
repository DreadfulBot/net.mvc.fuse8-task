namespace net.mvc.fuse8_task
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_OrderDetailTable
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int? OrderID { get; set; }

        public int? ProductID { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? Quantity { get; set; }

        public float? Discount { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowTimeStamp { get; set; }
    }
}
