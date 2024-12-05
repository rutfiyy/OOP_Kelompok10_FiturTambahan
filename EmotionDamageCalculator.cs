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
    }
}