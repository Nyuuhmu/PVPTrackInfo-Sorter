using System.Text;

namespace PVPTrackInfo_Sorter {

    public class PVPTrackInfo {

        public int Count;
        public int TrackCnt;
        public List<PVPTrackInfoEntry> Entries = new List<PVPTrackInfoEntry>();

        public static int[] CellLengths { get; } = new int[] {45, 50, 54};

        /// <summary>
        /// determines and returns the stage number of the provided file.
        /// </summary>
        /// <returns>int of the stage, or -1</returns>
        public static int FindStage(string _filePath) {
            string[] cells = File.ReadAllText(_filePath).Split(new string[] { "\t" }, StringSplitOptions.None);

            // There probably is a better way while still keeping drag & drop capabilities?
            if (cells[118] == "Comment") return 1;
            if (cells[133] == "Comment") return 2;
            if (cells[148] == "km/h") return 3;

            return -1;
        }

        /// <summary>
        /// reads the provided file and returns a <seealso cref="PVPTrackInfo"/> object.
        /// </summary>
        /// <returns>PVPTrackInfo</returns>
        public static PVPTrackInfo Read(string _filePath, int _stage) {
            int cellLength = CellLengths[_stage - 1];

            PVPTrackInfo pvpTrackInfo = new PVPTrackInfo();
            string[] cells = File.ReadAllText(_filePath).Split(new string[] { "\t" }, StringSplitOptions.None);

            // Row 2
            pvpTrackInfo.Count = int.Parse(cells[(cellLength * 1) + 1]);
            pvpTrackInfo.TrackCnt = int.Parse(cells[(cellLength * 1) + 2]);

            // All other Rows
            int row = 3;
            while (row < (cells.Length / cellLength)) {
                if (cells[(cellLength * row) + 2] != string.Empty) { // new Entry
                    PVPTrackInfoEntry entry = new PVPTrackInfoEntry();

                    entry.Lines.Add(PVPTrackInfoLine.ReadLine(cells, cellLength * row, _stage));
                    row++;

                    while (cells[(cellLength * row) + 2] == string.Empty) { // new Lines within an Entry
                        entry.Lines.Add(PVPTrackInfoLine.ReadLine(cells, cellLength * row, _stage));

                        row++;
                        if (row >= (cells.Length / cellLength)) break;
                    }

                    pvpTrackInfo.Entries.Add(entry);
                }
            }

            return pvpTrackInfo;
        }

        /// <summary>
        /// writes the provided <seealso cref="PVPTrackInfo"/> to a usable file.
        /// </summary>
        public static void Write(string _filePath, PVPTrackInfo _pvpTrackInfo, int _stage, Encoding _enc) {
            int cellLength = CellLengths[_stage - 1];
            StringBuilder output = new StringBuilder();

            output.Append("Preset\t");
            output.Append("Count\t");
            output.Append("TrackCnt\t");
            for (int j = 0; j < cellLength - 3; j++) { output.Append("\t"); }
            output.AppendLine();

            output.Append("Value\t");
            output.Append($"{_pvpTrackInfo.Count}\t");
            output.Append($"{_pvpTrackInfo.TrackCnt}\t");
            for (int j = 0; j < cellLength - 3; j++) { output.Append("\t"); }
            output.AppendLine();

            switch(_stage) {
                default:
                case 1: {
                        output.Append($"Index\tSupport\tcity\ttrack\tId\ttype\tTrackCnt\tgate\tnum\treward\tpos_start\t\tpos_end\t\tminx\tminy\tmaxx\tmaxy\tLapCount\tTraffic Preset\tReversePoint\tCityType\tcity\ttracknum\tReverse\tRecommend\tname\tMagic Number\tComment");
                        for (int j = 0; j < 17; j++) { output.Append("\t"); }
                    } break;
                case 2: {
                        output.Append($"Index\tSupport\tcity\ttrack\tId\ttype\tNew\tAI\tTrackCnt\tgate\tnum\treward\tpos_start\t\t\tpos_end\t\t\tminx\tminy\tmaxx\tmaxy\tLapCount\tTraffic Preset\tReversePoint\tCityType\tcity\ttracknum\tReverse\tRecommend\tMissionBattle\tname\tMagic Number\tComment");
                        for (int j = 0; j < 17; j++) { output.Append("\t"); }
                    } break;
                case 3: {
                        output.Append($"Index\tSupport\tcity\ttrack\tId\ttype\tNew\tAI\tTrackCnt\tgate\tnum\treward\tpos_start\t\t\tpos_end\t\t\tminx\tminy\tmaxx\tmaxy\tLapCount\tTraffic Preset\tReversePoint\tCityType\tcity\ttracknum\tReverse\tRecommend\tMissionBattle\tname\tMagic Number\tComment\tdx\tdy\t구간거리\t총거리\t최고기록\t최고평속ms\tkm/h");
                        for (int j = 0; j < 14; j++) { output.Append("\t"); }
                    } break;
            }
            output.AppendLine();

            foreach (var entry in _pvpTrackInfo.Entries) {
                foreach (var line in entry.Lines) {
                    output.Append($"{line.Index}\t");
                    output.Append($"{line.Support}\t");
                    output.Append($"{line.CityComment}\t");
                    output.Append($"{line.TrackFileName}\t");

                    string trackID = line.TrackID == int.MinValue ? "" : line.TrackID.ToString();
                    output.Append($"{trackID}\t");

                    output.Append($"{line.ObjectType}\t");

                    if (_stage != 1) {
                        output.Append($"{line.New}\t");
                        output.Append($"{line.AI}\t");
                    }

                    output.Append($"{line.TrackCount}\t");
                    output.Append($"{line.Gate}\t");
                    output.Append($"{line.Num}\t");
                    output.Append($"{line.Reward}\t");
                    output.Append($"{line.PosStartX}\t");
                    output.Append($"{line.PosStartY}\t");

                    if (_stage != 1) {
                        output.Append($"{line.PosStartZ}\t");
                    }
                    
                    output.Append($"{line.PosEndX}\t");
                    output.Append($"{line.PosEndY}\t");

                    if (_stage != 1) {
                        output.Append($"{line.PosEndZ}\t");
                    }

                    string minx = line.MinX == int.MinValue ? "" : line.MinX.ToString();
                    output.Append($"{minx}\t");

                    string miny = line.MinY == int.MinValue ? "" : line.MinY.ToString();
                    output.Append($"{miny}\t");

                    string maxx = line.MaxX == int.MinValue ? "" : line.MaxX.ToString();
                    output.Append($"{maxx}\t");

                    string maxy = line.MaxY == int.MinValue ? "" : line.MaxY.ToString();
                    output.Append($"{maxy}\t");

                    string lapCount = line.LapCount == int.MinValue ? "" : line.LapCount.ToString();
                    output.Append($"{lapCount}\t");

                    string trafficPreset = line.TrafficPreset == int.MinValue ? "" : line.TrafficPreset.ToString();
                    output.Append($"{trafficPreset}\t");

                    string reversePoint = line.ReversePoint == int.MinValue ? "" : line.ReversePoint.ToString();
                    output.Append($"{reversePoint}\t");

                    string cityType = line.CityType == int.MinValue ? "" : line.CityType.ToString();
                    output.Append($"{cityType}\t");

                    string city = line.City == int.MinValue ? "" : line.City.ToString();
                    output.Append($"{city}\t");

                    string tracknum = line.TrackNum == int.MinValue ? "" : line.TrackNum.ToString();
                    output.Append($"{tracknum}\t");

                    string reverse = line.Reverse == int.MinValue ? "" : line.Reverse.ToString();
                    output.Append($"{reverse}\t");

                    output.Append($"{line.Recommend}\t");

                    if (_stage != 1) {
                        output.Append($"{line.MissionBattle}\t");
                    }
                    
                    output.Append($"{line.GlobalTextString}\t");
                    output.Append($"{line.MagicNumber}\t");
                    output.Append($"{line.Comment}\t");

                    if (_stage == 3) {
                        string dx = line.DX == int.MinValue ? "" : line.DX.ToString();
                        output.Append($"{dx}\t");

                        string dy = line.DY == int.MinValue ? "" : line.DY.ToString();
                        output.Append($"{dy}\t");

                        string sectionDistance = line.SectionDistance == int.MinValue ? "" : line.SectionDistance.ToString();
                        output.Append($"{sectionDistance}\t");

                        string totalDistance = line.TotalDistance == int.MinValue ? "" : line.TotalDistance.ToString();
                        output.Append($"{totalDistance}\t");

                        output.Append($"{line.BestRecord}\t");
                        output.Append($"{line.TopSpeedMS}\t");
                        output.Append($"{line.KMH}\t");
                    }


                    switch (_stage) {
                        default:
                        case 1: {
                                for (int j = 0; j < 16; j++) { output.Append("\t"); }
                            } break;
                        case 2: {
                                for (int j = 0; j < 16; j++) { output.Append("\t"); }
                            } break;
                        case 3: {
                                for (int j = 0; j < 13; j++) { output.Append("\t"); }
                            } break;
                    }
                    output.AppendLine();
                }
            }

            File.WriteAllText(_filePath + $"_sorted.xlt", output.ToString(), _enc);
        }
    }
}
