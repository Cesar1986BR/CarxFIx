namespace Conserto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conserto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consertos",
                c => new
                    {
                        ConsertoId = c.Int(nullable: false, identity: true),
                        Defeito = c.String(),
                        Solucao = c.String(),
                        MecanicoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Usuario_UserId = c.Int(),
                        Pecas_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ConsertoId)
                .ForeignKey("dbo.Usuario", t => t.Usuario_UserId)
                .ForeignKey("dbo.Pecas", t => t.Pecas_Id)
                .ForeignKey("dbo.Usuario", t => t.ClienteId)
                .ForeignKey("dbo.Usuario", t => t.MecanicoId)
                .Index(t => t.MecanicoId)
                .Index(t => t.ClienteId)
                .Index(t => t.Usuario_UserId)
                .Index(t => t.Pecas_Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Telefone = c.String(nullable: false, maxLength: 20),
                        Endereco = c.String(nullable: false, maxLength: 100),
                        Photo = c.String(),
                        Cliente = c.Boolean(nullable: false),
                        Mecanico = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ConsertoDetalhes",
                c => new
                    {
                        consertoDetalhesId = c.Int(nullable: false, identity: true),
                        ConsertoId = c.Int(nullable: false),
                        PecaId = c.Int(nullable: false),
                        Usuario_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.consertoDetalhesId)
                .ForeignKey("dbo.Consertos", t => t.ConsertoId)
                .ForeignKey("dbo.Pecas", t => t.PecaId)
                .ForeignKey("dbo.Usuario", t => t.Usuario_UserId)
                .Index(t => t.ConsertoId)
                .Index(t => t.PecaId)
                .Index(t => t.Usuario_UserId);
            
            CreateTable(
                "dbo.Pecas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Photo = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Consertos", "MecanicoId", "dbo.Usuario");
            DropForeignKey("dbo.Consertos", "ClienteId", "dbo.Usuario");
            DropForeignKey("dbo.ConsertoDetalhes", "Usuario_UserId", "dbo.Usuario");
            DropForeignKey("dbo.ConsertoDetalhes", "PecaId", "dbo.Pecas");
            DropForeignKey("dbo.Consertos", "Pecas_Id", "dbo.Pecas");
            DropForeignKey("dbo.ConsertoDetalhes", "ConsertoId", "dbo.Consertos");
            DropForeignKey("dbo.Consertos", "Usuario_UserId", "dbo.Usuario");
            DropIndex("dbo.ConsertoDetalhes", new[] { "Usuario_UserId" });
            DropIndex("dbo.ConsertoDetalhes", new[] { "PecaId" });
            DropIndex("dbo.ConsertoDetalhes", new[] { "ConsertoId" });
            DropIndex("dbo.Consertos", new[] { "Pecas_Id" });
            DropIndex("dbo.Consertos", new[] { "Usuario_UserId" });
            DropIndex("dbo.Consertos", new[] { "ClienteId" });
            DropIndex("dbo.Consertos", new[] { "MecanicoId" });
            DropTable("dbo.Pecas");
            DropTable("dbo.ConsertoDetalhes");
            DropTable("dbo.Usuario");
            DropTable("dbo.Consertos");
        }
    }
}
