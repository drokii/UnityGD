using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Weapon
    {
        public int MaxAmmo;
        public int CurrentAmmo;
        public string Name;

        public int Damage { get; set; }

        public Weapon(EWeapon weapon)
        {
            if (weapon == EWeapon.Pistol)
            {
                MaxAmmo = 10;
                CurrentAmmo = MaxAmmo;
                Name = weapon.ToString();
                Damage = 25;
            }
            else if (weapon == EWeapon.Rifle)
            {
                MaxAmmo = 20;
                CurrentAmmo = MaxAmmo;
                Name = weapon.ToString();
                Damage = 50;
            }
            else
            {
                throw new Exception("Weapon doesn't exist.");
            }
        }

        public void Reload()
        {
            CurrentAmmo = MaxAmmo;
        }

    }
}
