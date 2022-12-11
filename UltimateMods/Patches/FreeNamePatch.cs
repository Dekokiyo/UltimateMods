// Taken from https://github.com/NuclearPowered/Reactor/ , licensed under the LGPLv3
// Source Code from TheOtherRoles(https://github.com/Eisbison/TheOtherRoles)
namespace UltimateMods.Patches
{
    internal class FreeNamePatch
    {
        public static void Initialize()
        {
            SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>)((scene, _) =>
            {
                if (!scene.name.Equals("MMOnline")) return;
                if (!TryMoveObjects()) return;

                var editName = FastDestroyableSingleton<AccountManager>.Instance.accountTab.editNameScreen;
                var nameText = Object.Instantiate(editName.nameText.gameObject);

                nameText.transform.localPosition += Vector3.up * 2.2f;

                var textBox = nameText.GetComponent<TextBoxTMP>();
                textBox.outputText.alignment = TextAlignmentOptions.CenterGeoAligned;
                textBox.outputText.transform.position = nameText.transform.position;
                textBox.outputText.fontSize = 4f;

                textBox.OnChange.AddListener((Action)(() =>
                {
                    AmongUs.Data.DataManager.Player.Customization.name = textBox.text;
                }));
                textBox.OnEnter = textBox.OnFocusLost = textBox.OnChange;

                textBox.Pipe.GetComponent<TextMeshPro>().fontSize = 4f;
            }));
        }

        private static bool TryMoveObjects()
        {
            var toMove = new List<string>
            {
                "HostGameButton",
                "FindGameButton",
                "JoinGameButton"
            };

            var yStart = Vector3.up;
            var yOffset = Vector3.down * 1.5f;

            var gameObjects = toMove.Select(x => GameObject.Find("NormalMenu/" + x)).ToList();
            if (gameObjects.Any(x => x == null)) return false;

            for (var i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].transform.position = yStart + (yOffset * i);
            }

            return true;
        }
    }
}