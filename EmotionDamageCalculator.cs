namespace OOP_Kelompok2
{
    public static class EmotionDamageCalculator
    {
        public static int CalculateDamage(int baseDamage, Emotion attackerEmotion, Emotion targetEmotion)
        {
            int damage = baseDamage;

            switch (attackerEmotion)
            {
                case Emotion.Happy:
                    if (targetEmotion == Emotion.Angry)
                    {
                        damage *= 2; // Double damage
                    }
                    else if (targetEmotion == Emotion.Sad)
                    {
                        damage /= 2; // Half damage
                    }
                    break;

                case Emotion.Angry:
                    if (targetEmotion == Emotion.Happy)
                    {
                        damage /= 2; // Half damage
                    }
                    else if (targetEmotion == Emotion.Sad)
                    {
                        damage *= 2; // Double damage
                    }
                    break;

                case Emotion.Sad:
                    if (targetEmotion == Emotion.Happy)
                    {
                        damage *= 2; // Double damage
                    }
                    else if (targetEmotion == Emotion.Angry)
                    {
                        damage /= 2; // Half damage
                    }
                    break;
            }

            return damage;
        }

        public static string AttackEffect(Emotion attackerEmotion, Emotion targetEmotion)
        {
            string effect = null;
            switch (attackerEmotion)
            {
                case Emotion.Happy:
                    if (targetEmotion == Emotion.Angry)
                    {
                        effect = "It was effective!";
                    }
                    else if (targetEmotion == Emotion.Sad)
                    {
                        effect = "It wasn't effective!";
                    }
                    break;

                case Emotion.Angry:
                    if (targetEmotion == Emotion.Happy)
                    {
                        effect = "It wasn't effective!";
                    }
                    else if (targetEmotion == Emotion.Sad)
                    {
                        effect = "It was effective!";
                    }
                    break;

                case Emotion.Sad:
                    if (targetEmotion == Emotion.Happy)
                    {
                        effect = "It was effective!";
                    }
                    else if (targetEmotion == Emotion.Angry)
                    {
                        effect = "It wasn't effective!";
                    }
                    break;
            }

            return effect;
        }
    }
}