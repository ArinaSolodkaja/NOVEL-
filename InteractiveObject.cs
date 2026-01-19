using System.Drawing;

namespace NOVEL_
{
    public class InteractiveObject
    {
        public string Name { get; }
        public string Description { get; }
        public Image Image { get; }
        public string SuspectName { get; }
        public bool IsCollected { get; set; }
        public int EvidenceValue { get; set; }

        public InteractiveObject(string name, string description, Image image, string suspectName, int evidenceValue = 1)
        {
            Name = name;
            Description = description;
            Image = image;
            SuspectName = suspectName;
            IsCollected = false;
            EvidenceValue = evidenceValue;
        }

        // ВСЕ УЛИКИ ПРОЕКТА

        // Улики против Алины Красновой
        public static InteractiveObject KnifeEvidence()
        {
            return new InteractiveObject(
                "Кинжал с гравировкой А.К.",
                "Старинный кинжал с гравировкой «А.К.» найден рядом с телом графа.\n" +
                "Инициалы соответствуют Алине Красновой, актрисе театра.",
                null,
                "Алина Краснова",
                2
            );
        }

        public static InteractiveObject TheatreTickets()
        {
            return new InteractiveObject(
                "Театральные билеты на имя А.К.",
                "Билеты в Мариинский театр на имя Алины Красновой,\n" +
                "обнаруженные в кабинете графа. Доказательство связи.",
                null,
                "Алина Краснова",
                1
            );
        }

        public static InteractiveObject LoveLetter()
        {
            return new InteractiveObject(
                "Любовное письмо от графа",
                "Неотправленное любовное письмо графа Воронцова к Алине Красновой.\n" +
                "Упоминает их тайные встречи в театре.",
                null,
                "Алина Краснова",
                1
            );
        }

        public static InteractiveObject TestimonyAgainstDoctor()
        {
            return new InteractiveObject(
                "Показания Красновой против доктора",
                "Алина Краснова утверждает, что видела доктора Львова,\n" +
                "убегающего от места преступления после убийства графа.",
                null,
                "Доктор Львов",
                2
            );
        }

        // Улики против Доктора Львова
        public static InteractiveObject PoisonedGlass()
        {
            return new InteractiveObject(
                "Бокал с ядом",
                "Бокал с остатками отравленного вина.\n" +
                "Содержит следы мышьяка - лекарства, которое выписывал доктор.",
                null,
                "Доктор Львов",
                3
            );
        }

        public static InteractiveObject MedicineBottle()
        {
            return new InteractiveObject(
                "Пузырек с микстурой доктора",
                "Лекарство от доктора Львова, найденное в библиотеке.\n" +
                "Химический состав совпадает с ядом в вине.",
                null,
                "Доктор Львов",
                2
            );
        }

        public static InteractiveObject MedicalRecords()
        {
            return new InteractiveObject(
                "Медицинские записи доктора",
                "Записи доктора Львова о лечении графа.\n" +
                "Последняя запись: \"Пациент отказывается от лечения. Опасно.\"",
                null,
                "Доктор Львов",
                1
            );
        }

        public static InteractiveObject FinancialDocuments()
        {
            return new InteractiveObject(
                "Долговая расписка",
                "Расписка о крупном долге доктора Львова графу Воронцову.\n" +
                "Сумма 50,000 рублей, срок выплаты - неделя до убийства.",
                null,
                "Доктор Львов",
                2
            );
        }

        // Улики против Графини Воронцовой
        public static InteractiveObject WillDocument()
        {
            return new InteractiveObject(
                "Завещание графа",
                "Черновик завещания, где всё состояние графа\n" +
                "переходит не графине, а Алине Красновой.",
                null,
                "Графиня Воронцова",
                3
            );
        }

        public static InteractiveObject JewelryReceipt()
        {
            return new InteractiveObject(
                "Квитанция из ломбарда",
                "Квитанция о залоге фамильных драгоценностей графиней\n" +
                "за месяц до убийства. Признак финансовых проблем.",
                null,
                "Графиня Воронцова",
                1
            );
        }

        public static InteractiveObject ServantTestimony()
        {
            return new InteractiveObject(
                "Показания горничной",
                "Горничная видела, как графиня спорила с графом\n" +
                "в ночь убийства. Слышала угрозы.",
                null,
                "Графиня Воронцова",
                2
            );
        }

        public static InteractiveObject HiddenPoison()
        {
            return new InteractiveObject(
                "Скрытый флакон с ядом",
                "Маленький флакон с мышьяком, найденный\n" +
                "в шкатулке графини. Тот же яд, что в бокале.",
                null,
                "Графиня Воронцова",
                3
            );
        }

        // Общие улики
        public static InteractiveObject UnfinishedLetter()
        {
            return new InteractiveObject(
                "Неоконченное письмо графа",
                "«Если со мной что‑то случится, ищите правду в...»\n" +
                "Письмо оборвано на середине предложения.",
                null,
                "Общая улика",
                1
            );
        }

        public static InteractiveObject OpenSafe()
        {
            return new InteractiveObject(
                "Открытый сейф",
                "Сейф в кабинете открыт и пуст.\n" +
                "Исчезли важные документы и деньги.",
                null,
                "Общая улика",
                1
            );
        }

        public static InteractiveObject BrokenLock()
        {
            return new InteractiveObject(
                "Сломанный замок на окне",
                "Окно в библиотеке было взломано изнутри.\n" +
                "Следы указывают на то, что кто-то пытался скрыться.",
                null,
                "Общая улика",
                1
            );
        }

        // Метод для отметки улики как собранной
        public void CollectEvidence()
        {
            IsCollected = true;
        }
    }
}