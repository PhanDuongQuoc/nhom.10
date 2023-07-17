namespace nhom._10
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHANVIEN")]
    public partial class NHANVIEN
    {
        [Key]
        [StringLength(100)]
        public string MANV { get; set; }

        [StringLength(30)]
        public string TENNV { get; set; }

        public DateTime ? NGAYSINH { get; set; }

        [StringLength(100)]
        public string MAPB { get; set; }

        public virtual PHONGBAN PHONGBAN { get; set; }
    }
}
