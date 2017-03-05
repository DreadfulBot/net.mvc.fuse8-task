namespace net.mvc.fuse8_task
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NorthwindModel : DbContext
    {
        public NorthwindModel()
            : base("name=NorthwindDatabase")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Shipper> Shipper { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<vw_CategoryTable> vw_CategoryTable { get; set; }
        public virtual DbSet<vw_CustomerOrdersProducts> vw_CustomerOrdersProducts { get; set; }
        public virtual DbSet<vw_CustomerOrderTotals> vw_CustomerOrderTotals { get; set; }
        public virtual DbSet<vw_CustomerTable> vw_CustomerTable { get; set; }
        public virtual DbSet<vw_EmployeeTable> vw_EmployeeTable { get; set; }
        public virtual DbSet<vw_OrderDetailTable> vw_OrderDetailTable { get; set; }
        public virtual DbSet<vw_OrderTable> vw_OrderTable { get; set; }
        public virtual DbSet<vw_ProductTable> vw_ProductTable { get; set; }
        public virtual DbSet<vw_ShipperTable> vw_ShipperTable { get; set; }
        public virtual DbSet<vw_SupplierTable> vw_SupplierTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.Freight)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetail)
                .WithOptional(e => e.Order)
                .WillCascadeOnDelete();

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Shipper>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<vw_CategoryTable>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<vw_CustomerOrdersProducts>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<vw_CustomerOrderTotals>()
                .Property(e => e.Freight)
                .HasPrecision(19, 4);

            modelBuilder.Entity<vw_CustomerTable>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<vw_EmployeeTable>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<vw_OrderDetailTable>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<vw_OrderDetailTable>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<vw_OrderTable>()
                .Property(e => e.Freight)
                .HasPrecision(19, 4);

            modelBuilder.Entity<vw_OrderTable>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<vw_ProductTable>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<vw_ProductTable>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<vw_ShipperTable>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<vw_SupplierTable>()
                .Property(e => e.RowTimeStamp)
                .IsFixedLength();
        }
    }
}
