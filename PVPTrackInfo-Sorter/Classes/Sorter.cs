using System.Text;

namespace PVPTrackInfo_Sorter {

    public class Sorter {

        /// <summary>
        /// checks which stage the provided file is and forwards it to be sorted.
        /// </summary>
        public static void Do(string _filePath, Encoding _enc) {
            int stage = PVPTrackInfo.FindStage(_filePath);

            if (stage == -1) {
                Console.WriteLine($"Unable to determine the Stage this file uses. Make sure the file uses vanilla organization.");
                return;
            } 

            PVPTrackInfo pvpTrackInfo = PVPTrackInfo.Read(_filePath, stage);
            PVPTrackInfo sortedPVPTrackInfo = Sort(pvpTrackInfo);

            PVPTrackInfo.Write(_filePath, sortedPVPTrackInfo, stage, _enc);
        }

        /// <summary>
        /// sorts the provided file.
        /// </summary>
        /// <returns>PVPTrackInfo</returns>
        public static PVPTrackInfo Sort(PVPTrackInfo _unsorted) {
            PVPTrackInfo sorted = new PVPTrackInfo();

            sorted.Count = _unsorted.Count;
            sorted.TrackCnt = _unsorted.TrackCnt;

            // Sort by City
            for (int i = 0; i <= 5; i++) {
                foreach (var entry in _unsorted.Entries) {
                    if (entry.Lines[0].City == i) {
                        sorted.Entries.Add(entry);
                    }
                }
            }

            // Apply new Index
            int counter = 0;
            foreach(var entry in sorted.Entries) {
                foreach (var line in entry.Lines) {
                    if (line.CityComment != string.Empty) Console.WriteLine($"#{counter} {line.GlobalTextString}");

                    line.Index = counter;
                    counter++;
                }
            }

            return sorted;
        }
    }
}
