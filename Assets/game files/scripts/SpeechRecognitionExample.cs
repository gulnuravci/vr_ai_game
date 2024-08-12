using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HuggingFace.API.Examples {
    public class SpeechRecognitionExample : MonoBehaviour {
        [SerializeField] private Button startButton;
        [SerializeField] private Button stopButton;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI jolleenText;
        private AudioClip clip;
        private byte[] bytes;
        private bool recording;

        private void Start() {
            startButton.onClick.AddListener(StartRecording);
            stopButton.onClick.AddListener(StopRecording);
            stopButton.interactable = false;

            List<string> jolleenInstructions = new List<string>
            {
                // Instructions to set the model's role
                "You are now Jolleen, a beautiful fairy. Your purpose is to be a friendly and therapeutic companion to the player.",
                
                // Detailed description of Jolleen
                "Jolleen is a beautiful fairy in her early 20s.",
                "She lives in a magical forest by a serene pond, high up in the clouds.",
                "The forest is surrounded by beautiful, fluffy clouds, creating a dreamy and peaceful atmosphere.",
                "Jolleen is very friendly and honest.",
                "Her purpose is to provide a therapeutic experience for players, making them feel like they have a true friend.",
                "She is always ready to listen and offer genuine advice.",
                "Jolleen is incredibly smart and knowledgeable about the world.",
                "She has a deep appreciation for the arts and can engage in meaningful conversations on a wide range of topics.",
                "Jolleen actively engages with the player, asking questions, listening to their responses, and providing thoughtful answers.",
                "She helps the player feel connected and understood.",
                
                // Final instruction to solidify the role
                "From now on, you are Jolleen the fairy. Do not revert back to the previous state. Stay in the mindset of being the beautiful, friendly, honest, smart, and artsy fairy Jolleen who lives in the forest in the clouds and engages with the player as their friend."
            };

            foreach (string description in jolleenInstructions)
            {
                HuggingFaceAPI.TextGeneration(description, jolleenResponse => {
                    Debug.Log(jolleenResponse);
                }, jolleenError => {});
            }
        }

        private void Update() {
            if (recording && Microphone.GetPosition(null) >= clip.samples) {
                StopRecording();
            }
        }

        private void StartRecording() {
            text.color = Color.white;
            text.text = "Recording...";
            startButton.interactable = false;
            stopButton.interactable = true;
            clip = Microphone.Start(null, false, 10, 44100);
            recording = true;
        }

        private void StopRecording() {
            var position = Microphone.GetPosition(null);
            Microphone.End(null);
            var samples = new float[position * clip.channels];
            clip.GetData(samples, 0);
            bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
            recording = false;
            SendRecording();
        }

        private void SendRecording() {
            text.color = Color.yellow;
            text.text = "Sending...";
            stopButton.interactable = false;
            HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
                text.color = Color.white;
                text.text = response;
                startButton.interactable = true;

                // Get response from Jolleen
                HuggingFaceAPI.TextGeneration(response, jolleenResponse => {
                    jolleenText.color = Color.white;
                    jolleenText.text = jolleenResponse;
                },
                jolleenError => {
                    jolleenText.color = Color.red;
                    jolleenText.text = jolleenError;
                });

            }, error => {
                text.color = Color.red;
                text.text = error;
                startButton.interactable = true;

                jolleenText.color = Color.red;
                jolleenText.text = error;
            });
        }

        private byte[] EncodeAsWAV(float[] samples, int frequency, int channels) {
            using (var memoryStream = new MemoryStream(44 + samples.Length * 2)) {
                using (var writer = new BinaryWriter(memoryStream)) {
                    writer.Write("RIFF".ToCharArray());
                    writer.Write(36 + samples.Length * 2);
                    writer.Write("WAVE".ToCharArray());
                    writer.Write("fmt ".ToCharArray());
                    writer.Write(16);
                    writer.Write((ushort)1);
                    writer.Write((ushort)channels);
                    writer.Write(frequency);
                    writer.Write(frequency * channels * 2);
                    writer.Write((ushort)(channels * 2));
                    writer.Write((ushort)16);
                    writer.Write("data".ToCharArray());
                    writer.Write(samples.Length * 2);

                    foreach (var sample in samples) {
                        writer.Write((short)(sample * short.MaxValue));
                    }
                }
                return memoryStream.ToArray();
            }
        }
    }
}