using NUnit.Framework;

[TestFixture]
public class ZombieTests
{
    [Test]
    public void TakeDamage_WhenHealthAboveZero_ShouldDecreaseHealthAndNotDestroyZombie()
    {
        // Arrange
        EnemyHealth enemyHealth = new EnemyHealth();
        int initialHealth = enemyHealth.getCurrentHealth();
        int damage = 10;

        // Act
        enemyHealth.TakeDamage(damage);

        // Assert
        Assert.AreEqual(initialHealth - damage, enemyHealth.getCurrentHealth());
        Assert.IsFalse(enemyHealth.IsDestroyed());
    }

    [Test]
    public void TakeDamage_WhenHealthBelowOrEqualToZero_ShouldDecreaseHealthAndDestroyZombie()
    {
        // Arrange
        EnemyHealth enemyHealth = new EnemyHealth();
        int initialHealth = enemyHealth.getCurrentHealth();
        int damage = initialHealth;

        // Act
        enemyHealth.TakeDamage(damage);

        // Assert
        Assert.AreEqual(0, enemyHealth.getCurrentHealth());
        Assert.IsTrue(enemyHealth.IsDestroyed());
    }

    [Test]
    public void TakeDamage_WhenKillCounterNotNull_ShouldIncreaseKillCount()
    {
        // Arrange
        EnemyHealth enemyHealth = new EnemyHealth();
        KillCounter killCounter = new KillCounter();
        enemyHealth.setKillCounter(killCounter);
        int initialKillCount = killCounter.GetKills();
        int damage = enemyHealth.getCurrentHealth();

        // Act
        enemyHealth.TakeDamage(damage);

        // Assert
        Assert.AreEqual(initialKillCount + 1, killCounter.GetKills());
    }
}