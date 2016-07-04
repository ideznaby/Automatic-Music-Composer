  // <copyright file="Form1.cs" company="ITdevelopers">
  // Copyright (c) 9/17/2014 All Right Reserved
  // </copyright>
  // <author>Iman Deznabi</author>
  // <date>9/17/2014</date>
  // <summary>A complete algorithmic composer</summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NAudio.Midi;
using System.Data.SqlClient;

namespace musiccomposerv0._5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public SqlConnection sqlc;
        public SqlCommand sc;
        public static List<int> notes = new List<int>();
        public static List<int> notenumbers = new List<int>();
        public static List<int> times = new List<int>();
        public static List<int> timenumbers = new List<int>();
        public static Dictionary<string, int> counts = new Dictionary<string, int>(); // tedade tekrare 1 ya masalan 2 va 1 2 poshte sare ham inja zakhire mishe (bigrams,trigrams,unigrams)
        public static Dictionary<string, int> timecounts = new Dictionary<string, int>();
        public static Dictionary<string, int> notetime = new Dictionary<string, int>();// bigramhaie time va note
        static double q(int v, int w, int u)
        {
            if (timecounts.ContainsKey(w + " " + u + " " + v))
                return ((double)timecounts[w + " " + u + " " + v] / timecounts[w + " " + u]);
            else
                return 0.0000001;
        }
        static double QML(int v, int w, int u)
        {
            double qml = 0;
            if (timecounts.ContainsKey(w + " " + u + " " + v))
                qml += ((double)timecounts[w + " " + u + " " + v] / timecounts[w + " " + u]);
            if (timecounts.ContainsKey(u + " " + v))
                qml += ((double)timecounts[u + " " + v] / timecounts[u + ""]);
            if (timecounts.ContainsKey(v + ""))
                qml += ((double)timecounts[v + ""] / times.Count);
            return qml / 3;
        }
        static double QML_notes(string u, string v, int w,int notenum,int numberofnotestocreate)
        {
            double qml = 0;
            if (counts.ContainsKey(u + " " + v + " " + w + "," + whichpart(notenum,numberofnotestocreate,5)))
                qml += ((double)counts[u + " " + v + " " + w + "," + whichpart(notenum, numberofnotestocreate, 5)] / counts[u + " " + v + "," + whichpart(notenum, numberofnotestocreate, 5)]);
            if (counts.ContainsKey(v + " " + w + "," + whichpart(notenum, numberofnotestocreate, 5)))
                qml += ((double)counts[v + " " + w + "," + whichpart(notenum, numberofnotestocreate, 5)] / counts[v + "," + whichpart(notenum, numberofnotestocreate, 5)]);
            if (counts.ContainsKey(w + "," + whichpart(notenum, numberofnotestocreate, 5)))
                qml += ((double)counts[w + "," + whichpart(notenum, numberofnotestocreate, 5)] / notes.Count);
            return qml / 3;
        }
        static double e(int note, int time)
        {
            if (notetime.ContainsKey(note + " " + time))
                return ((double)notetime[note + " " + time] / timecounts["" + time]);
            else
                return 0.0000001;
        }
        static void chorusdetector(MidiFile midi, int channel,int gap,int mincount,int intervals)
        {
            MidiEventCollection collection = new MidiEventCollection(midi.FileFormat, midi.DeltaTicksPerQuarterNote);
            int count=0,average=0,sum=0,localsum=0,localaverage=0;
            foreach (MidiEvent note in midi.Events[channel])
            {
                if (note.CommandCode == MidiCommandCode.NoteOn)
                {
                    int noteNumber = (note as NoteOnEvent).NoteNumber;
                    if ((note as NoteOnEvent).Velocity == 0)
                        continue;
                    count++;
                    sum += noteNumber;
                    average = sum / count;
                    if (count > mincount)
                    {
                        if (count % intervals == 0)
                        {
                            //if ((localaverage > average + gap) || (localaverage < average - gap))
                                //MessageBox.Show("this is chorus the time is : " + ((note as NoteOnEvent).AbsoluteTime/midi.DeltaTicksPerQuarterNote)*120 + " notename : " + (note as NoteOnEvent).NoteName);
                            localsum = 0;
                            localaverage = 0;
                            continue;
                        }
                        localsum += noteNumber;
                        localaverage = localsum / (count % intervals);
                    }
                }
            }
        } // tashkhise makane chorus
        static int[] viterbi(string[] notes)
        {
            int k = 1;
            Dictionary<string, double> PI = new System.Collections.Generic.Dictionary<string, double>();
            Dictionary<string, int> bp = new System.Collections.Generic.Dictionary<string, int>();
            int[] S = { -1 };
            PI.Add("0 -1 -1", 1);
            int[] timenumbersarray = timenumbers.ToArray();
            double max;
            int note;
            int[] output = new int[9999];
            double pi;
            int lasttime = 0;
            for (int c = 2; c < notes.Length; c++)
            {
                note = Convert.ToInt32(notes[c]);
                if (note == 0)
                {
                    continue;
                }
                foreach (int v in (k > 0 ? timenumbersarray : S))
                {
                    lasttime = v;
                    foreach (int u in (k - 1 > 0 ? timenumbersarray : S))
                    {
                        max = 0;
                        foreach (int w in (k - 2 > 0 ? timenumbersarray : S))
                        {
                            double E = e(note, v);
                            double Q = q(v, w, u);
                            if (E == 0.0000001 && Q == 0.0000001)
                                continue;
                            if (PI.ContainsKey((k - 1) + " " + w + " " + u))
                                if ((pi = PI[(k - 1) + " " + w + " " + u] * Q * E) + approximategoldenratio(u, v) >= max)
                                {
                                    //if(v == 145)
                                    // System.Diagnostics.Debugger.Break();
                                    //if (k > 4 && (PI[(k - 1) + " " + w + " " + u] != 0))
                                    //System.Diagnostics.Debugger.Break();
                                    // if (v == 18640 && u == 18640 && w == 18640 && k == 100)
                                    // System.Diagnostics.Debugger.Break();
                                    max = pi;
                                    PI[k + " " + u + " " + v] = max;
                                    bp[k + " " + u + " " + v] = w;
                                }
                        }
                    }
                }
                k++;
            }
            max = 0;
            foreach (int v in (k > 0 ? timenumbersarray : S))
                foreach (int u in (k - 1 > 0 ? timenumbersarray : S))
                {
                    if (PI.ContainsKey((k - 1) + " " + u + " " + v))
                        if ((pi = PI[(k - 1) + " " + u + " " + v] * q(lasttime, u, v)) >= max)
                        {
                            //if (pi != 0)
                            //System.Diagnostics.Debugger.Break();
                            max = pi;
                            output[k - 2] = u;
                            output[k - 1] = v;
                        }
                }
            for (int i = k - 3; i >= 1; i--)
                output[i] = bp[(i + 2) + " " + output[i + 1] + " " + output[i + 2]];
            return output;
        }
        static int[] nonviterbibesttimefinder(string[] notes)
        {
            int[] createdtimes = new int[notes.Length];
            createdtimes[0] = -1;
            createdtimes[1] = -1;
            int w = -1;
            int u = -1;
            double pi;
            int note;
            for (int i = 2; i < notes.Length; i++)
            {
                note = Convert.ToInt32(notes[i]);
                if (note == 0)
                {
                    continue;
                }
                double max = 0;
                int maxindex = 0;
                Random rnd = new Random();
                foreach (int v in timenumbers)
                {
                    if ((pi = QML(v, w, u) * e(note, v) - (0.1 * countoccur(createdtimes, new int[] { w, u, v }, 10)) + approximategoldenratio(u, v) + (0.001 * rnd.NextDouble())) >= max)
                    {
                        max = pi;
                        maxindex = v;
                    }
                }
                w = u;
                u = maxindex;
                createdtimes[i] = (maxindex);
            }
            return createdtimes;
        }
        public static void readmidi(MidiFile[] midis, int[] channels)
        {
            for (int i = 0; i < midis.Length; i++)
            {
                MidiFile midi = midis[i];
                chorusdetector(midi, channels[i], 5,10,5);
                notes.Add(-1);
                notes.Add(-1);
                times.Add(-1);
                times.Add(-1);
                //String filename = "Input/Another_Brick_in_the_Wall.mid";
                MidiEventCollection collection = new MidiEventCollection(midi.FileFormat, midi.DeltaTicksPerQuarterNote);
                /*for (int j = 0; j < midi.Events[3].Count; j++)
                    Console.WriteLine(midi.Events[3][j].ToString());
                Console.ReadKey();*/
                if (midi.FileFormat != 1)
                    continue;
                int sum = 0;
                double average = 0;
                int count = 0;
                foreach (MidiEvent note in midi.Events[channels[i]])
                {
                    //MessageBox.Show(note.ToString());
                    if (note.CommandCode == MidiCommandCode.NoteOn)
                    {
                        int noteNumber = (note as NoteOnEvent).NoteNumber;
                        if ((note as NoteOnEvent).Velocity == 0)
                            continue;
                        count++;
                        sum += noteNumber;
                        average = (double) sum / count;
                        times.Add((note as NoteOnEvent).NoteLength);
                        notes.Add(noteNumber);
                        if (!timenumbers.Contains((note as NoteOnEvent).NoteLength))
                            timenumbers.Add((note as NoteOnEvent).NoteLength);
                        if (!notenumbers.Contains(noteNumber))
                            notenumbers.Add(noteNumber);
                    }
                }
                times.Add(-2);
                notes.Add(-2);
            }
        }
        public static int whichpart(int notenum, int numberofnotes, int numberofparts)
        {
            for(int j=1;j<=numberofparts;j++)
            {
                if (notenum < (((double)j / numberofparts) * numberofnotes) && notenum > (((j - 1) / numberofparts) * numberofnotes))
                    return j;
            }
            return 0;
        }
        public static void count() // count trigram bigrams and unigrams
        {
            for (int i = 0; i < notes.Count; i++)
            {
                if (counts.ContainsKey(notes[i].ToString() + "," + whichpart(i, notes.Count, 5)))//adding unigram to counts
                    counts[notes[i].ToString() + "," + whichpart(i,notes.Count,5)]++;
                else
                    counts.Add(notes[i].ToString() + "," + whichpart(i, notes.Count, 5), 1);
                if (i < notes.Count - 1)//adding bigrams to counts
                {
                    if (counts.ContainsKey(notes[i] + " " + notes[i + 1] + "," + whichpart(i, notes.Count, 5)))
                        counts[(notes[i] + " " + notes[i + 1]) + "," + whichpart(i, notes.Count, 5)]++;
                    else
                        counts.Add((notes[i] + " " + notes[i + 1] + "," +  whichpart(i, notes.Count, 5)), 1);
                }
                if (i < notes.Count - 2)//adding trigram to counts
                {
                    if (counts.ContainsKey(notes[i] + " " + notes[i + 1] + " " + notes[i + 2] + "," + whichpart(i, notes.Count, 5)))
                        counts[(notes[i] + " " + notes[i + 1] + " " + notes[i + 2] + "," + whichpart(i, notes.Count, 5))]++;
                    else
                        counts.Add((notes[i] + " " + notes[i + 1] + " " + notes[i + 2] + "," + whichpart(i, notes.Count, 5)), 1);
                }
                if (timecounts.ContainsKey(times[i].ToString()))//adding time unigrams to timecounts
                    timecounts[times[i].ToString()]++;
                else
                    timecounts.Add(times[i].ToString(), 1);
                if (i < times.Count - 1)//adding time bigrams to timecounts
                {
                    if (timecounts.ContainsKey(times[i] + " " + times[i + 1]))
                        timecounts[(times[i] + " " + times[i + 1])]++;
                    else
                        timecounts.Add((times[i] + " " + times[i + 1]), 1);
                }
                if (i < times.Count - 2)//adding time trigrams to timecounts
                {
                    if (timecounts.ContainsKey(times[i] + " " + times[i + 1] + " " + times[i + 2]))
                        timecounts[(times[i] + " " + times[i + 1] + " " + times[i + 2])]++;
                    else
                        timecounts.Add((times[i] + " " + times[i + 1] + " " + times[i + 2]), 1);
                }
                if (notetime.ContainsKey(notes[i] + " " + times[i]))
                    notetime[(notes[i] + " " + times[i])]++;
                else
                    notetime.Add((notes[i] + " " + times[i]), 1);
            }
        }
        private static void AppendEndMarker(IList<MidiEvent> eventList)
        {
            long absoluteTime = 0;
            if (eventList.Count > 0)
                absoluteTime = eventList[eventList.Count - 1].AbsoluteTime;
            eventList.Add(new MetaEvent(MetaEventType.EndTrack, 0, absoluteTime));
        }
        public static void addaninstrument(MidiEventCollection events, int instrument, int channel)//adds an instrument to output midi file and building it's notes
        {
            /*for(int i=0;i<128;i++)
                Console.WriteLine(i + " : " + PatchChangeEvent.GetPatchName(i));
            Console.ReadKey();*/
            //TempoEvent te = new TempoEvent(500000 , 0);//changes bpm
            //TimeSignatureEvent tse = new TimeSignatureEvent(4, 2, 10, 20, (long) 0);
            PatchChangeEvent pce = new PatchChangeEvent(0, channel, instrument==127?0:instrument); //changes the instrument
            //events.AddEvent(te, 0);
            //events.AddEvent(tse, 0);
            events.AddEvent(pce, channel);
            String creatednotes = "-1 -1";
            int AbsoluteTime = 0;
            Random rnd = new Random();
            for (int i = 0; i < 200; i++)
            {
                double max = -10;
                int NoteNumber = 0;
                String[] splitednotes = creatednotes.Split(' ');
                double pi = 0;
                foreach (int note in notenumbers)// create midi using unigrams bigrams and trigrams
                {
                    double penalty = 0;
                        if (creatednotes.Contains(splitednotes[i] + " " + splitednotes[i + 1] + " " + note))
                            penalty = -0.1 * countoccur(creatednotes, splitednotes[i] + " " + splitednotes[i + 1] + " " + note, 30);
                        if ((pi = (double) QML_notes(splitednotes[i],splitednotes[i+1],note,i,200) + penalty + approximategoldenratio(Convert.ToInt32(splitednotes[i + 1]), Convert.ToInt32(note)) + (0.01 * rnd.NextDouble())) > max)//finding maximum pi
                        {
                            max = pi;
                            NoteNumber = note;
                        }
                }
                creatednotes += " " + NoteNumber;
            }
            string[] splitnotes = creatednotes.Split(' ');
            //int[] besttimes = viterbi(splitnotes);
            int[] besttimes = nonviterbibesttimefinder(splitnotes);
            for (int i = 2; i < 200; i++)
            {
                int Velocity = 127; // Velocity is from 0 which is considered off, to 127 which is the maximum
                int Duration = besttimes[i];
                NoteOnEvent note1On = new NoteOnEvent(AbsoluteTime,(instrument==127)?10:channel, Convert.ToInt32(splitnotes[i]), Velocity, Duration);

                NoteOnEvent note1Off = new NoteOnEvent(AbsoluteTime + Duration, (instrument == 127) ? 10 : channel, Convert.ToInt32(splitnotes[i]), 0, 0); // This is in effect a note off event – letting us know that the note can stop playing now.
                events[channel].Add(note1On);
                events[channel].Add(note1Off);
                AbsoluteTime += besttimes[i];
            }
            AppendEndMarker(events[channel]);
        }
        public static int findchannel(int instrument, MidiFile midi)
        {
            MidiEventCollection collection = new MidiEventCollection(midi.FileFormat, midi.DeltaTicksPerQuarterNote);
            for (int i = 0; i < midi.Tracks; i++)
            {
                for (int j = 0; j < midi.Events[i].Count; j++)
                {
                    if (midi.Events[i][j].CommandCode == MidiCommandCode.PatchChange)
                        if ((midi.Events[i][j] as PatchChangeEvent).Patch == instrument)
                            return i;
                }
            }
            return -1;
        }
        public static void cleardics()
        {
            notes.Clear();
            notenumbers.Clear();
            times.Clear();
            timenumbers.Clear();
            counts.Clear();
            timecounts.Clear();
            notetime.Clear();
        }
        public static double approximategoldenratio(int a, int b)
        {
            int c = Math.Max(a, b);
            int d = Math.Min(a, b);
            if (d == 0)
                return 0;
            double GR = 1.61803398875;
            double distanceGR = 2 - Math.Pow((c / d) - GR, 2);
            if (distanceGR < 0)
                return 0;
            else
                return 0.001 * (distanceGR / 2);
        }
        static int countoccur(string sentence, string key, int range)
        {
            int C = 0;
            for (int i = (sentence.Length - key.Length - range < 0) ? 0 : sentence.Length - key.Length - range; i <= sentence.Length - key.Length; i++)
            {
                bool occur = true;
                for (int j = i; j < i + key.Length; j++)
                {
                    if (sentence[j] != key[j - i])
                        occur = false;
                }
                if (occur)
                    C++;
            }
            return C;
        }
        static int countoccur(int[] sentence, int[] key, int range)
        {
            int C = 0;
            for (int i = (sentence.Length - key.Length - range < 0) ? 0 : sentence.Length - key.Length - range; i <= sentence.Length - key.Length; i++)
            {
                bool occur = true;
                for (int j = i; j < i + key.Length; j++)
                {
                    if (sentence[j] != key[j - i])
                        occur = false;
                }
                if (occur)
                    C++;
            }
            return C;
        }
        static public void firsttest()
        {
            StreamReader sr = new StreamReader("input.txt");
            int[,] count = new int[10, 10];
            while (!sr.EndOfStream)
            {
                String[] line = sr.ReadLine().Split(' ');
                for (int i = 0; i < line.Length; i++)
                {
                    count[Convert.ToInt32(line[i]), i]++; // dar yek makan masalan 0 chanta 1 ya 2 ya 3 ... bude
                    if (counts.ContainsKey(line[i]))
                        counts[line[i]]++;
                    else
                        counts.Add(line[i], 1);
                    if (i != line.Length - 1)
                    {
                        if (counts.ContainsKey(line[i] + " " + line[i + 1]))
                            counts[(line[i] + " " + line[i + 1])]++;
                        else
                            counts.Add((line[i] + " " + line[i + 1]), 1);
                    }
                }
            }
            string sentence = "";
            string sentence1 = "1";
            for (int i = 0; i < count.GetLength(1); i++)// nesbat be count sentence ro dorost mikone
            {
                int max = 0;
                int maxindex = 0;
                for (int j = 0; j < count.GetLength(0); j++)
                {
                    if (count[j, i] > max)
                    {
                        max = count[j, i];
                        maxindex = j;
                    }
                }
                sentence += maxindex;
            }
            for (int i = 0; i < 10; i++)// nesbat be counts(bigramha) sentece1 ro dorost mikone
            {
                double max = 0;
                int maxindex = 0;
                for (int j = 1; j < 10; j++)
                {
                    double penalty = 0;
                    if (counts.ContainsKey(sentence1[i] + " " + j) && counts.ContainsKey("" + sentence1[i]))
                    {
                        if (sentence1.Contains(sentence1[i] + "" + j))
                            penalty = -0.5 * countoccur(sentence1, sentence1[i] + "" + j, 10);
                        if (((double)counts[sentence1[i] + " " + j] / counts["" + sentence1[i]]) + penalty > max)
                        {
                            max = ((double)counts[sentence1[i] + " " + j] / counts["" + sentence1[i]]) + penalty;
                            maxindex = j;
                        }
                    }
                }
                sentence1 += maxindex;
            }
            Console.WriteLine("sentence based on count : " + sentence);
            Console.WriteLine("sentence based on bigrams : " + sentence1);
            Console.ReadLine();
        }
        public string get_instruments()
        {
            string insts = "";
            for (int i = 0; i < instruments.SelectedItems.Count; i++)
            {
                insts += instruments.SelectedIndices[i] + (i==instruments.SelectedItems.Count-1?"":",");
            }
            return insts;
        }
        public string get_instrument_names()
        {
            string instnames = "";
            for (int i = 0; i < instruments.SelectedItems.Count; i++)
            {
                instnames += instruments.GetItemText(instruments.SelectedItems[i]) + (i == instruments.SelectedItems.Count - 1 ? "" : ",");
            }
            return instnames;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            selectedsongs.Items.Clear();
            sqlc = new SqlConnection("Data Source=localhost;Initial Catalog=musiccomposer;Integrated Security=True");
            try
            {
                sqlc.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("can't open connection to database");
            }
            MidiEventCollection events = new MidiEventCollection(1, 120);
            int outputTrackCount = instruments.SelectedItems.Count + 1;
            for (int track = 0; track < outputTrackCount; track++)
            {
                events.AddTrack();
            }
            AppendEndMarker(events[0]);
            DateTime T = DateTime.Now;
            string instrumentcommandstring;
            instrumentcommandstring = "SELECT link,channel#,inst# from musics,musicinstruments where musics.genre# = " + (genre.SelectedIndex+1) + " and musics.music#=musicinstruments.music# and inst# in ("+get_instruments() + ")";
            for (int i = 0; i < instruments.SelectedIndices.Count; i++)
                    instrumentcommandstring += " and musics.music# in (select music# from musicinstruments where inst# =" + instruments.SelectedIndices[i] + ")";
            instrumentcommandstring += " order by inst#";
            sc = new SqlCommand(instrumentcommandstring, sqlc);
            SqlDataReader myReader = sc.ExecuteReader();
            List<int> channels = new List<int>();
            List<String> filenames = new List<string>();
            while (myReader.Read())
            {
                if (!filenames.Contains(myReader.GetString(0)))
                {
                    filenames.Add(myReader.GetString(0));
                    selectedsongs.Items.Add(myReader.GetString(0));
                }
                channels.Add(myReader.GetInt32(1));
            }
            //String[] filenames = new string[] { "Input/biz_arls.mid", "Input/symphony_1_1_(c)lucarelli.mid" };
            MidiFile[] midis = new MidiFile[filenames.Count];
            for (int i = 0; i < filenames.Count; i++)
            {
                midis[i] = new MidiFile(filenames[i]);
            }
            for (int i = 0; i < instruments.SelectedIndices.Count; i++)
            {
                readmidi(midis, channels.GetRange(i * filenames.Count,filenames.Count).ToArray());
                count();
                addaninstrument(events, instruments.SelectedIndices[i], i+1);
                cleardics();
            }
            MidiFile.Export("..\\musics\\" + genre.GetItemText(genre.SelectedItem) + "_" + get_instrument_names() + "_" +  DateTime.Now.TimeOfDay.ToString().Replace(':','_').Replace('.','_').Replace('{','_').Replace('}','_') + ".mid", events);
            MessageBox.Show((DateTime.Now - T) + "");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'musiccomposerDataSet1.genres' table. You can move, or remove it, as needed.
            this.genresTableAdapter.Fill(this.musiccomposerDataSet1.genres);
            // TODO: This line of code loads data into the 'musiccomposerDataSet1.feelings' table. You can move, or remove it, as needed.
            this.feelingsTableAdapter.Fill(this.musiccomposerDataSet1.feelings);
            // TODO: This line of code loads data into the 'musiccomposerDataSet.instruments' table. You can move, or remove it, as needed.
            this.instrumentsTableAdapter.Fill(this.musiccomposerDataSet.instruments);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MidiFile chorusdetectormidi = new MidiFile(choruspath.Text);
            chorusdetector(chorusdetectormidi,Convert.ToInt32(choruschannel.Text),5,10,5);
        }
    }
}