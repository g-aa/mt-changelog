using Microsoft.EntityFrameworkCore;
using Mt.ChangeLog.Entities.Tables;
using Mt.Utilities;
using System;

namespace Mt.ChangeLog.Context
{
    /// <summary>
    /// Методы расширения для <see cref="MtContext"/>.
    /// </summary>
    public static class MtContextExtensions
    {
        /// <summary>
        /// Инициализировать состояние контекста данных по умолчанию.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        public static void InitializeDefaultState(this MtContext context)
        {
            if (Check.NotNull(context, nameof(context)).Database.EnsureCreated())
            {
                MtContextExtensions.AddDefaultEntities(context);
                MtContextExtensions.CreateViews(context);
                MtContextExtensions.CreateSqlFuncs(context);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Добавить сущности по умолчанию в базу данных <see cref="MtContext"/>.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        private static void AddDefaultEntities(MtContext context)
        {
            var armEdit = new ArmEdit()
            {
                Id = Guid.Parse("3E4DF70F-63EC-4101-8119-762B32464A27"),
                Date = DateTime.Now,
                DIVG = "ДИВГ.55101-00",
                Version = DefaultString.Version,
                Description = "ArmEdit по умолчанию.",
                Default = true,
                Removable = false,
            };
            context.ArmEdits.Add(armEdit);

            var author = new Author()
            {
                Id = Guid.Parse("1de61f12-7634-47cc-bcff-f146ca538f49"),
                FirstName = "-//-",
                LastName = "-//-",
                Position = "Автор по умолчанию.",
                Default = true,
                Removable = false,
            };
            context.Authors.Add(author);

            var protocol = new Protocol()
            {
                Id = Guid.Parse("275EAE7E-797D-4EA0-B8DF-915E155FD117"),
                Title = DefaultString.Protocol,
                Description = "Протокол информационного обмена по умолчанию.",
                Default = true,
                Removable = false,
            };
            var communication = new Communication()
            {
                Id = Guid.Parse("B7A2DA8E-2494-4C6C-BFB9-5CCA01E7EB1C"),
                Title = DefaultString.Communication,
                Description = "Тип коммуникационного модуля блока БМРЗ по умолчанию.",
                Default = true,
                Removable = false,
            };
            protocol.Communications.Add(communication);
            communication.Protocols.Add(protocol);
            context.Protocols.Add(protocol);
            context.Communications.Add(communication);

            var analogModule = new AnalogModule()
            {
                Id = Guid.Parse("3A90CF3A-B9E3-43F7-ABFD-0E4483A9FE55"),
                DIVG = DefaultString.DIVG,
                Title = DefaultString.AnalogModule,
                Current = DefaultString.Current,
                Description = "Аналоговый модуль блока БМРЗ по умолчанию.",
                Default = true,
                Removable = false,
            };
            var platform = new Platform()
            {
                Id = Guid.Parse("2405C011-D0F1-4FBF-9D1C-32E814DE7087"),
                Title = DefaultString.Platform,
                Description = "Платформа блока БМРЗ по умолчанию.",
                Default = true,
                Removable = false,
            };
            analogModule.Platforms.Add(platform);
            platform.AnalogModules.Add(analogModule);
            context.AnalogModules.Add(analogModule);
            context.Platforms.Add(platform);

            context.ProjectStatuses.AddRange(new ProjectStatus[]
            {
                new ProjectStatus()
                {
                    Id = Guid.Parse("6C19D2AD-B68F-4F30-A3C8-5E89263B5067"),
                    Title = "Внутренний",
                    Description = "Проект для внутреннего использования в блоках БМРЗ НТЦ Механотроника.",
                    Default = true,
                    Removable = false,
                },
                new ProjectStatus()
                {
                    Id = Guid.Parse("3c51f11b-3e0c-46c0-9673-a4e9f6384c7f"),
                    Title = "Актуальный",
                    Description = "Проект для коммерческого использования в блоках БМРЗ НТЦ Механотроника.",
                },
                new ProjectStatus()
                {
                    Id = Guid.Parse("e3174f9d-0820-41a6-abe5-1f8a2376f359"),
                    Title = "Тестовый",
                    Description = "Проект для проведения внутренних и внешних испытаний, сертификаций.",
                },
                new ProjectStatus()
                {
                    Id = Guid.Parse("f3e018df-6e03-4f2c-96f8-c166ade9ed18"),
                    Title = "Технологический",
                    Description = "Проект для использования в технологических стендах НТЦ Механотроника.",
                },
                new ProjectStatus()
                {
                    Id = Guid.Parse("502d78f6-015f-45df-aa57-7d952fc84549"),
                    Title = "Аннулированный",
                    Description = "Проект блока БМРЗ, поддержка и доработка которого прекращена в НТЦ Механотроника.",
                },
                new ProjectStatus()
                {
                    Id = Guid.Parse("6d1a530e-0f3e-476d-b633-208c929de09f"),
                    Title = "На аннулирование",
                    Description = "Проект блока БМРЗ, находящийся в процессе аннулирования и прекращения поддержки в НТЦ Механотроника.",
                },
            });

            context.RelayAlgorithms.AddRange(new RelayAlgorithm[]
            {
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("D2D7B8D8-6AEF-4D1C-A56D-99117B7040D6"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Самодиагностика БМРЗ",
                    LogicalNode = "-//-",
                    Description = "Алгоритм по умолчанию, самодиагностика блока БМРЗ.",
                    Default = true,
                    Removable = false,
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("5CFC833D-EFCA-40AA-8652-E7CC9B51610F"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Вызов",
                    LogicalNode = "-//-",
                    Description = "Алгоритм по умолчанию, вызывная сигнализация блока БМРЗ.",
                    Default = true,
                    Removable = false,
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("6417F76B-F1C7-4BFF-A550-5A0863F84B06"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Осциллограф",
                    LogicalNode = "-//-",
                    Description = "Алгоритм по умолчанию, осциллограф.",
                    Default = true,
                    Removable = false,
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("3942C6E4-7BE6-4B45-9897-20C655D5FE22"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Векторная диаграмма",
                    LogicalNode = "-//-",
                    Description = "Алгоритм по умолчанию, расчет векторных диаграмм.",
                    Default = true,
                    Removable = false,
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("71416188-61BE-42FC-9142-4DC5D26CDFE4"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Диагностика по пос. сост.",
                    LogicalNode = "-//-",
                    Description = "Алгоритм по умолчанию, диагностика аналогового модуля по постоянной составляющей сигнала.",
                    Default = true,
                    Removable = false,
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("31BCC150-731F-4109-81D0-42F837D2929D"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Фазировка",
                    LogicalNode = "-//-",
                    Description = "Алгоритм по умолчанию, проверка фазировки блока БМРЗ.",
                    Default = true,
                    Removable = false,
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("E655F180-FAC1-47CC-A4E6-A72AA3C3D754"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Пгр. смены уставок",
                    LogicalNode = "-//-",
                    Description = "Алгоритм по умолчанию, смена программы уставок блока БМРЗ.",
                    Default = true,
                    Removable = false,
                },
            });
        }

        /// <summary>
        /// Создать представления в базе данных <see cref="MtContext"/>.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        private static void CreateViews(MtContext context)
        {
            context.Database.ExecuteSqlRaw(
                @$"CREATE OR REPLACE VIEW ""{MtContext.Schema}"".""LastProjectsRevision"" AS
                WITH LastRevision AS(
                SELECT  pr.""ProjectVersionId"",
                        Max(pr.""Revision"") AS ""Revision""
                FROM ""{MtContext.Schema}"".""ProjectRevision"" pr
                GROUP BY pr.""ProjectVersionId""
                )
                SELECT  pr.""ProjectVersionId"" AS ""ProjectVersionId"",
                        pr.""Id"" AS ""ProjectRevisionId"",
                        pv.""Prefix"" AS ""Prefix"",
                        pv.""Title"" AS ""Title"",
                        pv.""Version"" AS ""Version"",
                        pr.""Revision"",
                        pl.""Title"" AS ""Platform"",
                        arm.""Version"" AS ""ArmEdit"",
                        pr.""Date""
                FROM LastRevision lr
                JOIN ""{MtContext.Schema}"".""ProjectRevision"" pr
                ON lr.""Revision"" = pr.""Revision""
                AND lr.""ProjectVersionId"" = pr.""ProjectVersionId""
                JOIN ""{MtContext.Schema}"".""ArmEdit"" arm
                ON arm.""Id"" = pr.""ArmEditId""
                JOIN ""{MtContext.Schema}"".""ProjectVersion"" pv
                ON pv.""Id"" = pr.""ProjectVersionId""
                JOIN ""{MtContext.Schema}"".""Platform"" pl
                ON pv.""PlatformId"" = pl.""Id"";");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE VIEW ""{MtContext.Schema}"".""AuthorContribution"" AS
                SELECT  CONCAT(athr.""LastName"", ' ', athr.""FirstName"") AS ""Author"",
                        count(athr.""Id"") AS ""Contribution""
                FROM ""{MtContext.Schema}"".""Author"" athr
                JOIN ""{MtContext.Schema}"".""ProjectRevisionAuthor"" pra
                ON athr.""Id"" = pra.""AuthorsId""
                GROUP BY athr.""LastName"", athr.""FirstName""
                ORDER BY ""Contribution"" DESC;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE VIEW ""{MtContext.Schema}"".""AuthorProjectContribution"" AS
                SELECT  CONCAT(athr.""LastName"", ' ', athr.""FirstName"") AS ""Author"",
                        pv.""Prefix"" AS ""ProjectPrefix"",
                        pv.""Title"" AS ""ProjectTitle"",
                        pv.""Version"" AS ""ProjectVersion"",
                        count(pv.""Id"") AS ""Contribution""
                FROM ""{MtContext.Schema}"".""Author"" athr
                JOIN ""{MtContext.Schema}"".""ProjectRevisionAuthor"" pra
                ON athr.""Id"" = pra.""AuthorsId""
                JOIN ""{MtContext.Schema}"".""ProjectRevision"" pr
                ON pr.""Id"" = pra.""ProjectRevisionsId""
                JOIN ""{MtContext.Schema}"".""ProjectVersion"" pv
                ON pv.""Id"" = pr.""ProjectVersionId""
                GROUP BY athr.""LastName"", athr.""FirstName"", pv.""Prefix"", pv.""Title"", pv.""Version""
                ORDER BY ""Author"" ASC, ""ProjectTitle"" ASC, ""ProjectPrefix"" ASC, ""ProjectVersion"" ASC;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE VIEW ""{MtContext.Schema}"".""ProjectHistoryRecord"" AS
                WITH SortProjectRevisionsAlgs AS(
                SELECT  ra.""Title"" AS ""Algorithm"",
                        pra.""ProjectRevisionsId""
                FROM ""{MtContext.Schema}"".""RelayAlgorithm"" ra
                JOIN ""{MtContext.Schema}"".""ProjectRevisionRelayAlgorithm"" pra
                ON ra.""Id"" = pra.""RelayAlgorithmsId""
                ORDER BY pra.""ProjectRevisionsId"" ASC, ra.""Title"" ASC
                ),
                ProjectRevisionsAlgs AS(
                SELECT  spra.""ProjectRevisionsId"" AS ""ProjectRevisionId"",
                            string_agg(spra.""Algorithm"", ', ') AS ""Algorithms""
                FROM SortProjectRevisionsAlgs spra
                GROUP BY spra.""ProjectRevisionsId""
                ),
                SortProjectRevisionsAuthors AS(
                SELECT  concat(athr.""LastName"", ' ', athr.""FirstName"") AS ""Author"",
                        pra.""ProjectRevisionsId""
                FROM ""{MtContext.Schema}"".""Author"" athr
                JOIN ""{MtContext.Schema}"".""ProjectRevisionAuthor"" pra
                ON athr.""Id"" = pra.""AuthorsId""
                ORDER BY pra.""ProjectRevisionsId"" ASC, ""Author"" ASC
                ),
                ProjectRevisionsAthrs AS(
                SELECT  spra.""ProjectRevisionsId"" AS ""ProjectRevisionId"",
                            string_agg(spra.""Author"", ', ') AS ""Authors""
                FROM SortProjectRevisionsAuthors spra
                GROUP BY spra.""ProjectRevisionsId""
                ),
                SortProjectRevisionsProtocols AS(
                SELECT  cmp.""CommunicationsId"",
                        prot.""Title"" AS ""Protocol""
                FROM ""{MtContext.Schema}"".""Protocol"" prot
                JOIN ""{MtContext.Schema}"".""CommunicationProtocol"" cmp
                ON prot.""Id"" = cmp.""ProtocolsId""
                ORDER BY cmp.""CommunicationsId"" ASC, prot.""Title"" ASC
                ),
                ProjectRevisionsProtocols AS(
                SELECT  sprp.""CommunicationsId"" AS ""CommunicationId"",
                            string_agg(sprp.""Protocol"", ', ') AS ""Protocols""
                FROM SortProjectRevisionsProtocols sprp
                GROUP BY sprp.""CommunicationsId""
                )
                SELECT  pv.""Id"" AS ""ProjectVersionId"",
                        pr.""ParentRevisionId"",
                        pr.""Id"" AS ""ProjectRevisionId"",
                        plat.""Title"" AS ""Platform"",
                        concat(pv.""Prefix"", '-', pv.""Title"", '-', pv.""Version"", '_', pr.""Revision"") AS ""Title"",
                        pr.""Date"",
                        arm.""Version"" AS ""ArmEdit"",
                        prAlgs.""Algorithms"",
                        prAthrs.""Authors"",
                        prProts.""Protocols"",
                        pr.""Reason"",
                        pr.""Description""
                FROM ""{MtContext.Schema}"".""ProjectRevision"" pr
                JOIN ""{MtContext.Schema}"".""ProjectVersion"" pv ON pv.""Id"" = pr.""ProjectVersionId""
                JOIN ""{MtContext.Schema}"".""ArmEdit"" arm ON arm.""Id"" = pr.""ArmEditId""
                JOIN ""{MtContext.Schema}"".""Platform"" plat ON plat.""Id"" = pv.""PlatformId""
                JOIN ProjectRevisionsAlgs prAlgs ON prAlgs.""ProjectRevisionId"" = pr.""Id""
                JOIN ProjectRevisionsAthrs prAthrs ON prAthrs.""ProjectRevisionId"" = pr.""Id""
                JOIN ProjectRevisionsProtocols prProts ON prProts.""CommunicationId"" = pr.""CommunicationId"";");
        }

        /// <summary>
        /// Создать представления в базе данных <see cref="MtContext"/>.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        private static void CreateSqlFuncs(MtContext context)
        {
            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_ActualArmEdit""()
                    RETURNS TABLE(""Id"" uuid, ""Version"" character, ""DIVG"" character, ""Date"" timestamp without time zone, ""Description"" character varying) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                SELECT	arm.""Id"",
			                arm.""Version"",
			                arm.""DIVG"",
			                arm.""Date"",                    
			                arm.""Description""
	                FROM ""{MtContext.Schema}"".""ArmEdit"" arm
	                ORDER BY arm.""Version"" DESC
	                LIMIT 1;
                $BODY$;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_AnalogModule""(guid uuid)
                    RETURNS TABLE(""Id"" uuid, ""Title"" character, ""DIVG"" character, ""Current"" character, ""Description"" character varying) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                SELECT 	am.""Id"",
			                am.""Title"",
		                    am.""DIVG"",
                        	am.""Current"",
		                    am.""Description""
	                FROM ""{MtContext.Schema}"".""AnalogModule"" am
	                WHERE am.""Id"" = ""guid"";       
                $BODY$;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_ArmEdit""(guid uuid)
                    RETURNS TABLE(""Id"" uuid, ""Version"" character, ""DIVG"" character, ""Date"" timestamp without time zone, ""Description"" character varying) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                SELECT	arm.""Id"",
  			                arm.""Version"",
                            arm.""DIVG"",
                            arm.""Date"",                    
                            arm.""Description""
	                FROM ""{MtContext.Schema}"".""ArmEdit"" arm
                    WHERE arm.""Id"" = guid;
                $BODY$;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_PlatformsForAnalogModule""(guid uuid)
                    RETURNS TABLE(""Id"" uuid, ""Title"" character) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                WITH AmPlatforms AS(
		                SELECT pam.""PlatformsId""
                        FROM ""{MtContext.Schema}"".""PlatformAnalogModule"" pam
                        WHERE pam.""AnalogModulesId"" = ""guid""
                    )                              
	                SELECT p.""Id"",
		                   p.""Title""
  	                FROM ""{MtContext.Schema}"".""Platform"" p
  	                JOIN AmPlatforms amp
  	                ON p.""Id"" = amp.""PlatformsId"";
                $BODY$;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_ShortAnalogModules""()
                    RETURNS TABLE(""Id"" uuid, ""Title"" character) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                SELECT 	am.""Id"",
           	                am.""Title""
	                FROM ""{MtContext.Schema}"".""AnalogModule"" am;
                $BODY$;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_ShortArmEdits""()
                    RETURNS TABLE(""Id"" uuid, ""Version"" character) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                SELECT 	arm.""Id"", 
    		                arm.""Version""
	                FROM ""{MtContext.Schema}"".""ArmEdit"" arm;
                $BODY$;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_TableAnalogModules""()
                    RETURNS TABLE(""Id"" uuid, ""Title"" character, ""DIVG"" character, ""Current"" character, ""Description"" character varying) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                SELECT 	am.""Id"",
           	                am.""Title"",
		 	                am.""DIVG"",
		 	                am.""Current"",
		 	                am.""Description""
  	                FROM ""{MtContext.Schema}"".""AnalogModule"" am;
                $BODY$;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_TableArmEdits""()
                    RETURNS TABLE(""Id"" uuid, ""Version"" character, ""DIVG"" character, ""Date"" timestamp without time zone, ""Description"" character varying) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                SELECT	arm.""Id"",
    		                arm.""Version"",
                            arm.""DIVG"",
                            arm.""Date"",                    
                            arm.""Description""
	                FROM ""{MtContext.Schema}"".""ArmEdit"" arm;
                $BODY$;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_Author""(guid uuid)
                    RETURNS TABLE(""Id"" uuid, ""FirstName"" character varying, ""LastName"" character varying, ""Position"" character varying) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                SELECT	a.""Id"",
                            a.""FirstName"",
                            a.""LastName"",
                            a.""Position""                    
	                FROM ""{MtContext.Schema}"".""Author"" a
	                WHERE a.""Id"" = guid;          
                $BODY$;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_ShortAuthors""()
                    RETURNS TABLE(""Id"" uuid, ""FirstName"" character varying, ""LastName"" character varying) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                SELECT	a.""Id"",
                            a.""FirstName"",
                            a.""LastName""
                    FROM ""{MtContext.Schema}"".""Author"" a;            
                $BODY$;");

            context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE FUNCTION ""{MtContext.Schema}"".""get_TableAuthors""()
                    RETURNS TABLE(""Id"" uuid, ""FirstName"" character varying, ""LastName"" character varying, ""Position"" character varying) 
                    LANGUAGE 'sql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                    ROWS 1000
                AS $BODY$
	                SELECT	a.""Id"",
                            a.""FirstName"",
                            a.""LastName"",
			                a.""Position""
                    FROM ""{MtContext.Schema}"".""Author"" a;            
                $BODY$;");
        }
    }
}