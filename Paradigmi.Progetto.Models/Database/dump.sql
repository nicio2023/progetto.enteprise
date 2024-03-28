CREATE TABLE [dbo].[Utente](
	[IdUtente] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Cognome] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Utente] PRIMARY KEY CLUSTERED 
(
	[IdUtente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CategoriaLibro]  WITH CHECK ADD  CONSTRAINT [FK_CategoriaLibro_Categoria] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoriaLibro] CHECK CONSTRAINT [FK_CategoriaLibro_Categoria]
GO
ALTER TABLE [dbo].[CategoriaLibro]  WITH CHECK ADD  CONSTRAINT [FK_CategoriaLibro_Libro] FOREIGN KEY([IdLibro])
REFERENCES [dbo].[Libro] ([IdLibro])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoriaLibro] CHECK CONSTRAINT [FK_CategoriaLibro_Libro]
GO
USE [master]
GO