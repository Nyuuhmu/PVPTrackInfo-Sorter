namespace PVPTrackInfo_Sorter {

    public class PVPTrackInfoLine {

        public int Index;
        public int Support;
        public string? CityComment;
        public string? TrackFileName;
        public int TrackID;
        public int ObjectType;
        public int New;
        public int AI;
        public int TrackCount;
        public string? Gate;
        public int Num;
        public int Reward;
        public float PosStartX;
        public float PosStartY;
        public float PosStartZ;
        public float PosEndX;
        public float PosEndY;
        public float PosEndZ;
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;
        public int LapCount;
        public int TrafficPreset;
        public int  ReversePoint;
        public int CityType;
        public int City;
        public int TrackNum;
        public int Reverse;
        public string? Recommend;
        public string? MissionBattle;
        public string? GlobalTextString;
        public int MagicNumber;
        public string? Comment;
        public float DX;
        public float DY;
        public float SectionDistance;
        public float TotalDistance;
        public string? BestRecord;
        public string? TopSpeedMS;
        public string? KMH;

        public static PVPTrackInfoLine ReadLine(string[] cells, int _position, int _stage) {
            PVPTrackInfoLine line = new PVPTrackInfoLine();

            int chki;
            float chkf;

            line.Index = int.TryParse(cells[_position + 0], out chki) ? chki : int.MinValue;
            line.Support = int.TryParse(cells[_position + 1], out chki) ? chki : int.MinValue;
            line.CityComment = cells[_position + 2];
            line.TrackFileName = cells[_position + 3];
            line.TrackID = int.TryParse(cells[_position + 4], out chki) ? chki : int.MinValue;
            line.ObjectType = int.TryParse(cells[_position + 5], out chki) ? chki : int.MinValue;

            if (_stage != 1) {
                line.New = int.TryParse(cells[_position + 6], out chki) ? chki : int.MinValue;
                line.AI = int.TryParse(cells[_position + 7], out chki) ? chki : int.MinValue;
            } else { _position -= 2; }

            line.TrackCount = int.TryParse(cells[_position + 8], out chki) ? chki : int.MinValue;
            line.Gate = cells[_position + 9];
            line.Num = int.TryParse(cells[_position + 10], out chki) ? chki : int.MinValue;
            line.Reward = int.TryParse(cells[_position + 11], out chki) ? chki : int.MinValue;
            line.PosStartX = float.TryParse(cells[_position + 12], out chkf) ? chkf : int.MinValue;
            line.PosStartY = float.TryParse(cells[_position + 13], out chkf) ? chkf : int.MinValue;

            if (_stage != 1) {
                line.PosStartZ = float.TryParse(cells[_position + 14], out chkf) ? chkf : int.MinValue;
            } else { _position -= 1; }

            line.PosEndX = float.TryParse(cells[_position + 15], out chkf) ? chkf : int.MinValue;
            line.PosEndY = float.TryParse(cells[_position + 16], out chkf) ? chkf : int.MinValue;

            if (_stage != 1) {
                line.PosEndZ = float.TryParse(cells[_position + 17], out chkf) ? chkf : int.MinValue;
            } else { _position -= 1; }

            line.MinX = float.TryParse(cells[_position + 18], out chkf) ? chkf : int.MinValue;
            line.MinY = float.TryParse(cells[_position + 19], out chkf) ? chkf : int.MinValue;
            line.MaxX = float.TryParse(cells[_position + 20], out chkf) ? chkf : int.MinValue;
            line.MaxY = float.TryParse(cells[_position + 21], out chkf) ? chkf : int.MinValue;
            line.LapCount = int.TryParse(cells[_position + 22], out chki) ? chki : int.MinValue;
            line.TrafficPreset = int.TryParse(cells[_position + 23], out chki) ? chki : int.MinValue;
            line.ReversePoint = int.TryParse(cells[_position + 24], out chki) ? chki : int.MinValue;
            line.CityType = int.TryParse(cells[_position + 25], out chki) ? chki : int.MinValue;
            line.City = int.TryParse(cells[_position + 26], out chki) ? chki : int.MinValue;
            line.TrackNum = int.TryParse(cells[_position + 27], out chki) ? chki : int.MinValue;
            line.Reverse = int.TryParse(cells[_position + 28], out chki) ? chki : int.MinValue;
            line.Recommend = cells[_position + 29];

            if (_stage != 1) {
                line.MissionBattle = cells[_position + 30];
            } else { _position -= 1; }

            line.GlobalTextString = cells[_position + 31];
            line.MagicNumber = int.TryParse(cells[_position + 32], out chki) ? chki : int.MinValue;
            line.Comment = cells[_position + 33];

            if (_stage == 3) {
                line.DX = float.TryParse(cells[_position + 34], out chkf) ? chkf : int.MinValue;
                line.DY = float.TryParse(cells[_position + 35], out chkf) ? chkf : int.MinValue;
                line.SectionDistance = float.TryParse(cells[_position + 36], out chkf) ? chkf : int.MinValue;
                line.TotalDistance = float.TryParse(cells[_position + 37], out chkf) ? chkf : int.MinValue;
                line.BestRecord = cells[_position + 38];
                line.TopSpeedMS = cells[_position + 39];
                line.KMH = cells[_position + 40];
            }

            return line;
        }
    }
}
