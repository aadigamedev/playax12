using System;
using GTANetworkAPI;

namespace ServerSide
{
    public class Commands : Script
    {
        [Command("car")]
        public void CreateCarCommand(Player player, string vehicleName)
        {
            if (string.IsNullOrEmpty(vehicleName))
            {
                player.SendChatMessage("Usage: /car [vehicle_name]");
                return;
            }

            try
            {
                VehicleHash vehicleHash = NAPI.Util.VehicleNameToModel(vehicleName);
                if (vehicleHash == 0)
                {
                    player.SendChatMessage($"~r~Error: Vehicle '{vehicleName}' does not exist.");
                    return;
                }

                Vector3 spawnPosition = player.Position.Around(5.0f);
                Vehicle vehicle = NAPI.Vehicle.CreateVehicle(vehicleHash, spawnPosition, player.Rotation.Z, 0, 0, "RP CAR");

                player.SetIntoVehicle(vehicle, (int)VehicleSeat.Driver);
                player.SendChatMessage($"~g~Vehicle '{vehicleName}' has been created and assigned to you.");
            }
            catch (Exception ex)
            {
                player.SendChatMessage($"~r~An error occurred: {ex.Message}");
            }
        }

        [Command("position", Alias = "pos")]
        public void PositionCommand(Player player)
        {
            try
            {
                Vector3 pos = player.Position;
                float heading = player.Rotation.Z;

                player.SendChatMessage($"~b~Position: ~w~X: {pos.X:F4}, Y: {pos.Y:F4}, Z: {pos.Z:F4}");
                player.SendChatMessage($"~b~Heading: ~w~{heading:F4}");
                player.SendChatMessage($"~g~Vector3: ~w~new Vector3({pos.X:F4}f, {pos.Y:F4}f, {pos.Z:F4}f)");
                Console.WriteLine($"~g~Vector3: ~w~new Vector3({pos.X:F4}f, {pos.Y:F4}f, {pos.Z:F4}f)");
            }
            catch (Exception ex)
            {
                player.SendChatMessage($"~r~Error: {ex.Message}");
            }
        }

        [Command("moongravity")]
        public void MoonGravityCommand(Player player)
        {
            try
            {
                // Toggle moon gravity
                bool isMoonGravityEnabled = player.HasSharedData("MoonGravity") && player.GetSharedData<bool>("MoonGravity");

                if (isMoonGravityEnabled)
                {
                    player.ResetSharedData("MoonGravity");
                    player.SendChatMessage("~g~Moon gravity disabled.");
                    player.TriggerEvent("ToggleMoonGravity", false);
                }
                else
                {
                    player.SetSharedData("MoonGravity", true);
                    player.SendChatMessage("~g~Moon gravity enabled! Enjoy your moon jumps!");
                    player.TriggerEvent("ToggleMoonGravity", true);
                }
            }
            catch (Exception ex)
            {
                player.SendChatMessage($"~r~An error occurred: {ex.Message}");
            }
        }

        [Command("gun")]
        public void GiveWeaponCommand(Player player, string weaponHashName)
        {
            if (string.IsNullOrEmpty(weaponHashName))
            {
                player.SendChatMessage("Usage: /gun [weapon_hash]");
                return;
            }

            try
            {
                WeaponHash weaponHash;
                if (!Enum.TryParse(weaponHashName, true, out weaponHash))
                {
                    player.SendChatMessage($"~r~Error: Weapon '{weaponHashName}' does not exist.");
                    return;
                }

                player.GiveWeapon(weaponHash, 250); // Give the weapon with 250 ammo
                player.SendChatMessage($"~g~Weapon '{weaponHashName}' has been added to your inventory.");
            }
            catch (Exception ex)
            {
                player.SendChatMessage($"~r~An error occurred: {ex.Message}");
            }
        }
    }
}
