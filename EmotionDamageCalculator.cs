namespace OOP_Kelompok2
{
    public static class EmotionDamageCalculator
    {
        public static int CalculateDamage(int baseDamage, Emotion attackerEmotion, Emotion targetEmotion)
        {
            int damage = baseDamage;

            if (attackerEmotion == Emotion.Happy && targetEmotion == Emotion.Angry)
            {
                damage *= 2; // Double damage
            }
            else if (attackerEmotion == Emotion.Angry && targetEmotion == Emotion.Happy)
            {
                damage /= 2; // Half damage
            }
            else if (attackerEmotion == Emotion.Angry && targetEmotion == Emotion.Sad)
            {
                damage *= 2; // Double damage
            }
            else if (attackerEmotion == Emotion.Sad && targetEmotion == Emotion.Angry)
            {
                damage /= 2; // Half damage
            }
            else if (attackerEmotion == Emotion.Sad && targetEmotion == Emotion.Happy)
            {
                damage *= 2; // Double damage
            }
            else if (attackerEmotion == Emotion.Happy && targetEmotion == Emotion.Sad)
            {
                damage /= 2; // Half damage
            }

            return damage;
        }
    }
}