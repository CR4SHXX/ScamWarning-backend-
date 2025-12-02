using Microsoft.EntityFrameworkCore;
using ScamWarning.Data;
using ScamWarning.Interfaces;
using ScamWarning.Repositories;
using ScamWarning.Services;
using ScamWarning.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure DbContext with SQLite Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ScamWarning.db"));

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IWarningRepository, WarningRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Register Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IWarningService, WarningService>();
builder.Services.AddScoped<ICommentService, CommentService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ScamWarning API",
        Version = "v1",
        Description = "API for managing scam warnings, user authentication, and comments"
    });
});

var app = builder.Build();

// Seed the database with initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Ensure database is created
    context.Database.EnsureCreated();
    
    // Seed Categories
    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category { Id = 1, Name = "Phishing", Emoji = "🎣", Description = "Email and website scams that steal personal information" },
            new Category { Id = 2, Name = "Phone Scam", Emoji = "📞", Description = "Fraudulent phone calls and SMS messages" },
            new Category { Id = 3, Name = "Investment Scam", Emoji = "💰", Description = "Fake investment opportunities and Ponzi schemes" },
            new Category { Id = 4, Name = "Romance Scam", Emoji = "💔", Description = "Online dating and romance fraud" },
            new Category { Id = 5, Name = "Other", Emoji = "⚠️", Description = "Other types of scams" }
        );
    }
    
    // Seed users
    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User 
            { 
                Id = 1, 
                Username = "demo", 
                Email = "demo@test.com", 
                Password = "demo123",
                IsAdmin = false,
                CreatedAt = DateTime.UtcNow
            },
            new User 
            { 
                Id = 2, 
                Username = "root", 
                Email = "root@admin.com", 
                Password = "dadababa",
                IsAdmin = true,
                CreatedAt = DateTime.UtcNow
            }
        );
    }
    
    // Seed sample warnings
    if (!context.Warnings.Any())
    {
        context.Warnings.AddRange(
            // Phishing Scams (CategoryId = 1)
            new Warning 
            { 
                Id = 1, 
                Title = "Faux email Bank Al-Maghrib",
                Description = "J'ai reçu un email prétendant venir de Bank Al-Maghrib demandant de vérifier mon compte en cliquant sur un lien. Le lien menait vers un faux site qui ressemblait exactement au vrai site de la banque.",
                WarningSigns = "Email non officiel, lien suspect, demande d'informations personnelles, fautes d'orthographe",
                ImageUrl = "",
                CategoryId = 1,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Warning 
            { 
                Id = 2, 
                Title = "Attijariwafa Bank compte suspendu",
                Description = "SMS reçu disant que mon compte Attijariwafa est suspendu et que je dois cliquer sur un lien pour le réactiver. Le lien demandait mon numéro de carte et code secret.",
                WarningSigns = "SMS avec lien, menace de suspension, demande de données bancaires",
                ImageUrl = "",
                CategoryId = 1,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-14)
            },
            new Warning 
            { 
                Id = 3, 
                Title = "BMCE Bank vérification urgente",
                Description = "Email urgent de 'BMCE Bank' demandant une vérification immédiate sous 24h sinon le compte sera fermé. L'adresse email était bmce-security@gmail.com au lieu du domaine officiel.",
                WarningSigns = "Urgence artificielle, adresse email Gmail, menaces",
                ImageUrl = "",
                CategoryId = 1,
                AuthorId = 2,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-13)
            },
            new Warning 
            { 
                Id = 4, 
                Title = "CIH Bank mise à jour obligatoire",
                Description = "Message WhatsApp avec logo CIH Bank demandant de mettre à jour mes informations via un lien. Le site demandait le code OTP reçu par SMS.",
                WarningSigns = "Message WhatsApp non officiel, demande de code OTP, lien raccourci",
                ImageUrl = "",
                CategoryId = 1,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-12)
            },
            new Warning 
            { 
                Id = 5, 
                Title = "Faux site Barid Bank",
                Description = "Publicité Facebook menant vers un faux site Barid Bank offrant des crédits à 0%. Le site collectait les données personnelles et bancaires.",
                WarningSigns = "Offre trop belle, publicité Facebook, URL différente de l'officielle",
                ImageUrl = "",
                CategoryId = 1,
                AuthorId = 2,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-11)
            },
            
            // Phone Scams (CategoryId = 2)
            new Warning 
            { 
                Id = 6, 
                Title = "Arnaque Inwi recharge gratuite",
                Description = "Appel prétendant être Inwi offrant 100 DH de recharge gratuite. Ils demandaient mon code secret et le code de recharge pour 'activer' l'offre.",
                WarningSigns = "Offre gratuite suspecte, demande de codes secrets, numéro inconnu",
                ImageUrl = "",
                CategoryId = 2,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new Warning 
            { 
                Id = 7, 
                Title = "Faux appel Maroc Telecom",
                Description = "Quelqu'un se faisant passer pour un technicien Maroc Telecom appelant pour 'réparer' ma ligne. Il demandait accès à mon téléphone à distance.",
                WarningSigns = "Appel non sollicité, demande d'accès à distance, pression",
                ImageUrl = "",
                CategoryId = 2,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-9)
            },
            new Warning 
            { 
                Id = 8, 
                Title = "Orange Maroc loterie gagnant",
                Description = "SMS disant que j'ai gagné 50,000 DH à la loterie Orange. Pour récupérer le prix, je devais envoyer 500 DH de frais de dossier via Wafacash.",
                WarningSigns = "Loterie non participée, demande de paiement préalable, SMS suspect",
                ImageUrl = "",
                CategoryId = 2,
                AuthorId = 2,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-8)
            },
            new Warning 
            { 
                Id = 9, 
                Title = "Arnaque WhatsApp code OTP",
                Description = "Message WhatsApp d'un 'ami' demandant de lui renvoyer un code reçu par SMS. C'était en fait un code de vérification WhatsApp pour voler mon compte.",
                WarningSigns = "Demande de code SMS, compte ami piraté, urgence",
                ImageUrl = "",
                CategoryId = 2,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new Warning 
            { 
                Id = 10, 
                Title = "Arnaque Wafacash transfert",
                Description = "Appel prétendant être Wafacash disant qu'un transfert m'attend mais je dois d'abord payer des 'taxes' de 200 DH pour le débloquer.",
                WarningSigns = "Demande de paiement pour recevoir argent, appel non officiel",
                ImageUrl = "",
                CategoryId = 2,
                AuthorId = 2,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            },
            
            // Investment Scams (CategoryId = 3)
            new Warning 
            { 
                Id = 11, 
                Title = "Crypto Binance Maroc arnaque",
                Description = "Groupe Telegram 'Binance Maroc Officiel' promettant de doubler les investissements crypto. Après avoir envoyé 5000 DH en Bitcoin, plus de nouvelles.",
                WarningSigns = "Promesses de gains garantis, groupe non officiel, pression pour investir vite",
                ImageUrl = "",
                CategoryId = 3,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new Warning 
            { 
                Id = 12, 
                Title = "Faux trading forex Casablanca",
                Description = "Formation trading forex à Casablanca promettant 10,000 DH/mois garantis. Après paiement de 3000 DH, la formation était des vidéos YouTube gratuites.",
                WarningSigns = "Gains garantis, formation payante suspecte, témoignages faux",
                ImageUrl = "",
                CategoryId = 3,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            },
            new Warning 
            { 
                Id = 13, 
                Title = "Investissement immobilier Tanger",
                Description = "Offre d'investissement immobilier à Tanger avec rendement 30% par an garanti. Après versement, la société a disparu.",
                WarningSigns = "Rendement irréaliste, pas de documents officiels, pression",
                ImageUrl = "",
                CategoryId = 3,
                AuthorId = 2,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            },
            new Warning 
            { 
                Id = 14, 
                Title = "MLM pyramide Marrakech",
                Description = "Opportunité 'business' à Marrakech demandant 2000 DH pour rejoindre et recruter d'autres personnes. Système pyramidal classique.",
                WarningSigns = "Recrutement obligatoire, frais d'entrée, promesses de revenus passifs",
                ImageUrl = "",
                CategoryId = 3,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            
            // Romance Scams (CategoryId = 4)
            new Warning 
            { 
                Id = 15, 
                Title = "Arnaque mariage étranger",
                Description = "Rencontre en ligne avec quelqu'un prétendant être un(e) MRE voulant se marier. Après quelques semaines, demande d'argent pour 'visa' ou 'billet d'avion'.",
                WarningSigns = "Relation rapide, jamais de vidéo, demandes d'argent",
                ImageUrl = "",
                CategoryId = 4,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Warning 
            { 
                Id = 16, 
                Title = "Faux profil Facebook Rabat",
                Description = "Profil Facebook avec photos volées prétendant être de Rabat. Après avoir gagné ma confiance, demande d'aide financière urgente pour 'maladie'.",
                WarningSigns = "Photos trop parfaites, histoire triste, demande d'argent",
                ImageUrl = "",
                CategoryId = 4,
                AuthorId = 2,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow
            },
            new Warning 
            { 
                Id = 17, 
                Title = "Demande Western Union urgent",
                Description = "Relation en ligne depuis 2 mois. La personne dit être bloquée à l'étranger et demande un transfert Western Union urgent de 8000 DH.",
                WarningSigns = "Urgence, Western Union, jamais rencontré en personne",
                ImageUrl = "",
                CategoryId = 4,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddHours(-12)
            },
            
            // Other Scams (CategoryId = 5)
            new Warning 
            { 
                Id = 18, 
                Title = "Faux colis Amana Express",
                Description = "SMS Amana Express disant qu'un colis m'attend mais je dois payer 49 DH de frais de douane via un lien. Le lien volait les données de carte bancaire.",
                WarningSigns = "Colis inattendu, paiement en ligne suspect, lien raccourci",
                ImageUrl = "",
                CategoryId = 5,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddHours(-8)
            },
            new Warning 
            { 
                Id = 19, 
                Title = "Loterie MRE gagnant",
                Description = "Email disant que j'ai gagné à une loterie pour les MRE organisée par le gouvernement. Demande de payer des frais de 1000 DH pour récupérer 100,000 DH.",
                WarningSigns = "Loterie non existante, paiement préalable, email suspect",
                ImageUrl = "",
                CategoryId = 5,
                AuthorId = 2,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddHours(-6)
            },
            new Warning 
            { 
                Id = 20, 
                Title = "Fausse offre emploi Gulf",
                Description = "Offre d'emploi au Qatar/UAE avec salaire 20,000 DH. Demande de payer 5000 DH pour 'visa de travail' et 'frais d'agence'. Arnaque classique.",
                WarningSigns = "Emploi trop beau, paiement de frais, pas d'entretien sérieux",
                ImageUrl = "",
                CategoryId = 5,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddHours(-4)
            },
            new Warning 
            { 
                Id = 21, 
                Title = "Arnaque location Avito",
                Description = "Appartement à louer sur Avito à prix très bas. Le 'propriétaire' à l'étranger demande un virement pour réserver avant visite.",
                WarningSigns = "Prix trop bas, propriétaire absent, paiement avant visite",
                ImageUrl = "",
                CategoryId = 5,
                AuthorId = 2,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddHours(-2)
            },
            new Warning 
            { 
                Id = 22, 
                Title = "Faux concours Instagram Maroc",
                Description = "Compte Instagram 'Maroc Giveaway' demandant de partager et payer 50 DH pour participer à un tirage iPhone. Aucun gagnant n'a jamais été annoncé.",
                WarningSigns = "Paiement pour participer, compte récent, pas de gagnants vérifiables",
                ImageUrl = "",
                CategoryId = 5,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow.AddHours(-1)
            }
        );
    }
    
    context.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection disabled for development (frontend uses HTTP)
// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();