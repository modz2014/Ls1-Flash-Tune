using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LS1_FLASH_TOOL_V2.XX
{
	public class Form1 : Form
	{
		private int a = 0;

		private int b = 0;

		private int c = 0;

		private int d = 0;

		private int w = 0;

		private int x = 0;

		private int y = 0;

		private int z = 0;

		private int cancel = 0;

		private int binoffset = 0;

		private int length = 0;

		private int chksumlsb = 0;

		private int chksummsb = 0;

		private int modulelength0 = 0;

		private int modulelength1 = 0;

		private int modulelength2 = 0;

		private int modulelength3 = 0;

		private int modulelength4 = 0;

		private int modulelength5 = 0;

		private int modulelength6 = 0;

		private int modulelength7 = 0;

		private int moduleNoBeingWritten = 0;

		private int moduleMessageSize = 0;

		private int calfileoffset = 0;

		private int osfileoffset = 0;

		private int numbermessagefillerbytes = 0;

		private int timeout = 0;

		private byte[] vpwmessagerx5 = new byte[6];

		private byte[] vpwmessagerx6 = new byte[6];

		private byte[] vpwmessagerx7 = new byte[7];

		private byte[] vpwmessagerx8 = new byte[8];

		private byte[] vpwmessagerx10 = new byte[10];

		private byte[] vpwmessagerx = new byte[12];

		private byte[] vpwmessagerx17 = new byte[17];

		private byte[] vpwmessagetx2062 = new byte[8290];

		private byte[] binread = new byte[1048576];

		private byte[] module0 = new byte[4];

		private byte[] module1 = new byte[4];

		private byte[] module2 = new byte[4];

		private byte[] module3 = new byte[4];

		private byte[] module4 = new byte[4];

		private byte[] module5 = new byte[4];

		private byte[] module6 = new byte[4];

		private byte[] seed = new byte[2];

		private byte[] key = new byte[2];

		private byte[] readaddress = new byte[2];

		private byte[] binfile = new byte[1048576];

		private byte[] calfile = new byte[98304];

		private byte[] osfile512 = new byte[409600];

		private byte[] osfile1024 = new byte[933888];

		private byte[] osfile512copy = new byte[409600];

		private byte[] osfile1024copy = new byte[933888];

		private byte[] modulestart0 = new byte[4];

		private byte[] modulestart1 = new byte[4];

		private byte[] modulestart2 = new byte[4];

		private byte[] modulestart3 = new byte[4];

		private byte[] modulestart4 = new byte[4];

		private byte[] modulestart5 = new byte[4];

		private byte[] modulestart6 = new byte[4];

		private byte[] modulestart7 = new byte[4];

		private byte[] moduleend0 = new byte[4];

		private byte[] moduleend1 = new byte[4];

		private byte[] moduleend2 = new byte[4];

		private byte[] moduleend3 = new byte[4];

		private byte[] moduleend4 = new byte[4];

		private byte[] moduleend5 = new byte[4];

		private byte[] moduleend6 = new byte[4];

		private byte[] moduleend7 = new byte[4];

		private byte[] binfileosnumber = new byte[4];

		private byte[] pcmosnumber = new byte[4];

		private byte[] securitywritebytes = new byte[94];

		private byte[] vinbuffer = new byte[18];

		private byte[] serialnobuffer = new byte[13];

		private byte[] serialnowritebytes = new byte[26];

		private byte[] oschecksum = new byte[2];

		private byte[] calculatedoschksum = new byte[2];

		private string buffer0;

		private string buffer1;

		private string Chosen_File;

		private string BinFileName;

		private string CodePrefix;

		private char[] vin = new char[17];

		private char[] serialno = new char[12];

		private char[] bcc = new char[4];

		private IContainer components = null;

		private ComboBox comboBoxComPort;

		private TextBox textBox1;

		private Button buttonPCMDetails;

		private Label label1;

		private TextBox textBoxSeed;

		private Label label2;

		private TextBox textBoxKey;

		private SerialPort serialPort1;

		private TextBox textBoxModule0;

		private TextBox textBoxModule1;

		private TextBox textBoxModule2;

		private TextBox textBoxModule3;

		private TextBox textBoxModule4;

		private TextBox textBoxModule5;

		private TextBox textBoxModule6;

		private TextBox textBoxModule7;

		private TextBox textBoxVIN;

		private TextBox textBoxSerialNumber;

		private TextBox textBoxBCC;

		private TextBox textBoxPCMHardwareNo;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label label9;

		private Label label10;

		private Label label11;

		private Label label12;

		private Label label13;

		private Label label14;

		private Label label15;

		private Button buttonCancel;

		private Button buttonReadBin;

		private System.Windows.Forms.Timer timerReadBin;

		private SaveFileDialog saveFileDialog1;

		private ProgressBar progressBar1;

		private ComboBox comboBoxFlashType;

		private Button buttonWriteCal;

		private System.Windows.Forms.Timer timerWriteCal;

		private Button buttonWriteBin;

		private System.Windows.Forms.Timer timerWriteBin;

		private Button buttonWriteSec;

		private Button buttonWriteVin;

		private Button buttonWriteSerial;

		private Button buttonWriteKey;

		private System.Windows.Forms.Timer timerDisableChatter;

		private OpenFileDialog openFileDialog1;

		private Label label17;

		private Button buttonReadDTC;

		private Button buttonClearDTC;

		private Label label16;

		public Form1()
		{
			InitializeComponent();
			string[] portNames = SerialPort.GetPortNames();
			int num = 0;
			comboBoxComPort.Items.Clear();
			string[] array = portNames;
			foreach (string text in array)
			{
				comboBoxComPort.Items.Add(text);
				num++;
				if (num == 1)
				{
					comboBoxComPort.Text = text;
				}
			}
			if (comboBoxComPort.SelectedItem == null)
			{
				textBox1.AppendText("No Com Port Available");
				textBox1.AppendText("\n");
			}
		}

		private void OPENPORT(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				bool flag = false;
				serialPort1.PortName = comboBoxComPort.Text;
				serialPort1.BaudRate = 115200;
				try
				{
					serialPort1.Open();
				}
				catch (UnauthorizedAccessException)
				{
					flag = true;
				}
				catch (IOException)
				{
					flag = true;
				}
				catch (ArgumentException)
				{
					flag = true;
				}
				if (flag)
				{
					MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					cancel = 1;
				}
				else if (!serialPort1.IsOpen)
				{
				}
			}
		}

		private void CLOSEPORT(object sender, EventArgs e)
		{
			if (serialPort1.IsOpen)
			{
				serialPort1.Close();
			}
		}

		private void comboBoxComPort_DropDown(object sender, EventArgs e)
		{
			string[] portNames = SerialPort.GetPortNames();
			int num = 0;
			comboBoxComPort.Items.Clear();
			string[] array = portNames;
			foreach (string text in array)
			{
				comboBoxComPort.Items.Add(text);
				num++;
				if (num == 1)
				{
					comboBoxComPort.Text = text;
				}
			}
			if (comboBoxComPort.SelectedItem == null)
			{
				textBox1.AppendText("No Com Port Available");
				textBox1.AppendText("\n");
			}
		}

		private string ByteArrayToHexString(byte[] bufferx)
		{
			StringBuilder stringBuilder = new StringBuilder(bufferx.Length * 3);
			foreach (byte value in bufferx)
			{
				stringBuilder.Append(Convert.ToString(value, 16).PadLeft(2, '0').PadRight(3, ' '));
			}
			return stringBuilder.ToString().ToUpper();
		}

		private byte[] HexStringToByteArray(string s)
		{
			s = s.Replace(" ", "");
			s = s.Replace("\n ", "");
			byte[] array = new byte[s.Length / 2];
			for (int i = 0; i < s.Length; i += 2)
			{
				array[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
			}
			return array;
		}

		private int ByteArrayToInt32(byte[] s)
		{
			int num = 0;
			num = s[0] * 16777216 + s[1] * 65536 + s[2] * 256 + s[3];
			return Convert.ToInt32(num);
		}

		private byte[] Int32ToByteArray32(int s)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			byte[] array = new byte[4];
			while (s > 255)
			{
				s -= 256;
				num3++;
				if (num3 == 256)
				{
					num3 = 0;
					num2++;
				}
				if (num2 == 256)
				{
					num2 = 0;
					num++;
				}
			}
			array[0] = Convert.ToByte(num);
			array[1] = Convert.ToByte(num2);
			array[2] = Convert.ToByte(num3);
			array[3] = Convert.ToByte(s);
			return array;
		}

		private void SENDBINTOFILE(object sender, EventArgs e)
		{
			saveFileDialog1.Filter = ".bin Files|*.bin|*.bin|*.*";
			if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
			{
				Invoke(new EventHandler(CLOSEPORT));
				return;
			}
			string fileName = saveFileDialog1.FileName;
			if (!File.Exists(fileName))
			{
				FileStream fileStream = new FileStream(fileName, FileMode.CreateNew);
				fileStream.Close();
			}
			FileStream fileStream2 = new FileStream(fileName, FileMode.Open);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream2);
			if (comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B")
			{
				binaryWriter.Write(binread, 0, 524288);
				binaryWriter.Close();
				fileStream2.Close();
			}
			else if (comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B")
			{
				binaryWriter.Write(binread, 0, 1048576);
				binaryWriter.Close();
				fileStream2.Close();
			}
		}

		private void OPENBINFILE(object sender, EventArgs e)
		{
			if (cancel != 0)
			{
				return;
			}
			openFileDialog1.Filter = ".bin |*.bin|.bin|*.bin*";
			if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
			{
				cancel = 1;
				return;
			}
			Chosen_File = openFileDialog1.FileName;
			BinFileName = openFileDialog1.SafeFileName;
			textBox1.AppendText("Selected Bin File");
			textBox1.AppendText("\n");
			textBox1.AppendText(BinFileName);
			textBox1.AppendText("\n");
			textBox1.AppendText("\n");
			length = Chosen_File.Length;
			FileStream fileStream = new FileStream(Chosen_File, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			length = (int)fileStream.Length;
			if (comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B")
			{
				binfile = binaryReader.ReadBytes(524288);
				binaryReader.Close();
				fileStream.Close();
			}
			if (comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B")
			{
				binfile = binaryReader.ReadBytes(1048576);
				binaryReader.Close();
				fileStream.Close();
			}
			if ((comboBoxFlashType.Text != "512KB A") & (comboBoxFlashType.Text != "512KB B") & (comboBoxFlashType.Text != "1024KB A") & (comboBoxFlashType.Text != "1024KB B"))
			{
				MessageBox.Show(this, "PCM Type Unknown \nSelect PCM Type Manually \nor\nClick pcm Details", "PCM Type Unknown", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				cancel = 1;
			}
		}

		private void CALCULATEOSCHECKSUM(object sender, EventArgs e)
		{
			byte[] array;
			if (comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B")
			{
				x = 0;
				y = 0;
				array = osfile512copy;
				foreach (byte b in array)
				{
					osfile512copy[x] = osfile512[y];
					x++;
					y++;
				}
				oschecksum[0] = osfile512copy[1280];
				oschecksum[1] = osfile512copy[1281];
				osfile512copy[1280] = 0;
				osfile512copy[1281] = 0;
				osfile512copy[409598] = 0;
				osfile512copy[409599] = 0;
				x = 0;
				z = 0;
				chksumlsb = 0;
				chksummsb = 0;
				array = osfile512copy;
				foreach (byte b in array)
				{
					if (z == 0)
					{
						chksummsb += b;
						if (chksummsb > 255)
						{
							chksummsb -= 256;
						}
					}
					if (z == 1)
					{
						chksumlsb += b;
						if (chksumlsb > 255)
						{
							chksummsb++;
							chksumlsb -= 256;
							if (chksummsb > 255)
							{
								chksummsb -= 256;
							}
						}
					}
					if (z == 0)
					{
						z = 1;
					}
					else
					{
						z = 0;
					}
				}
				x = 0;
				y = 0;
				calculatedoschksum[0] = Convert.ToByte(chksummsb);
				calculatedoschksum[1] = Convert.ToByte(chksumlsb);
				x = calculatedoschksum[0] * 256 + calculatedoschksum[1];
				x = 65536 - x;
				while (x > 255)
				{
					x -= 256;
					y++;
					if (y > 255)
					{
						y = 0;
					}
				}
				calculatedoschksum[0] = Convert.ToByte(y);
				calculatedoschksum[1] = Convert.ToByte(x);
				if (calculatedoschksum[0] != oschecksum[0])
				{
					MessageBox.Show(this, "Bin Contains Corrupt Operating System Checksum. ", "Bin File Checksum Error ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					cancel = 1;
					return;
				}
				if (calculatedoschksum[1] != oschecksum[1])
				{
					MessageBox.Show(this, "Bin Contains Corrupt Operating System Checksum. ", "Bin File Checksum Error ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					cancel = 1;
					return;
				}
			}
			if (!(comboBoxFlashType.Text == "1024KB A") && !(comboBoxFlashType.Text == "1024KB B"))
			{
				return;
			}
			x = 0;
			y = 0;
			array = osfile1024copy;
			foreach (byte b in array)
			{
				osfile1024copy[x] = osfile1024[y];
				x++;
				y++;
			}
			oschecksum[0] = osfile1024copy[1280];
			oschecksum[1] = osfile1024copy[1281];
			osfile1024copy[1280] = 0;
			osfile1024copy[1281] = 0;
			osfile1024copy[933886] = 0;
			osfile1024copy[933887] = 0;
			x = 0;
			z = 0;
			chksumlsb = 0;
			chksummsb = 0;
			array = osfile1024copy;
			foreach (byte b in array)
			{
				if (z == 0)
				{
					chksummsb += b;
					if (chksummsb > 255)
					{
						chksummsb -= 256;
					}
				}
				if (z == 1)
				{
					chksumlsb += b;
					if (chksumlsb > 255)
					{
						chksummsb++;
						chksumlsb -= 256;
						if (chksummsb > 255)
						{
							chksummsb -= 256;
						}
					}
				}
				if (z == 0)
				{
					z = 1;
				}
				else
				{
					z = 0;
				}
			}
			x = 0;
			y = 0;
			calculatedoschksum[0] = Convert.ToByte(chksummsb);
			calculatedoschksum[1] = Convert.ToByte(chksumlsb);
			x = calculatedoschksum[0] * 256 + calculatedoschksum[1];
			x = 65536 - x;
			while (x > 255)
			{
				x -= 256;
				y++;
				if (y > 255)
				{
					y = 0;
				}
			}
			calculatedoschksum[0] = Convert.ToByte(y);
			calculatedoschksum[1] = Convert.ToByte(x);
			if (calculatedoschksum[0] != oschecksum[0])
			{
				MessageBox.Show(this, "Bin Contains Corrupt Operating System Checksum. ", "Bin File Checksum Error ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				cancel = 1;
			}
			else if (calculatedoschksum[1] != oschecksum[1])
			{
				MessageBox.Show(this, "Bin Contains Corrupt Operating System Checksum. ", "Bin File Checksum Error ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				cancel = 1;
			}
		}

		private void GETCALFILEDATA(object sender, EventArgs e)
		{
			if ((comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B") && length != 1048576)
			{
				MessageBox.Show(this, "PCM Type / Bin File Length Mismatch \nCheck PCM Type And Bin File", "Incorrect Bin File Length", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				cancel = 1;
			}
			else if ((comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B") && length != 524288)
			{
				MessageBox.Show(this, "PCM Type / Bin File Length Mismatch \nCheck PCM Type And Bin File", "Incorrect Bin File Length", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				cancel = 1;
			}
			else if (cancel == 0)
			{
				binfileosnumber[0] = binfile[1284];
				binfileosnumber[1] = binfile[1285];
				binfileosnumber[2] = binfile[1286];
				binfileosnumber[3] = binfile[1287];
				x = 32768;
				y = 0;
				byte[] array = calfile;
				foreach (byte b in array)
				{
					calfile[y] = binfile[x];
					y++;
					x++;
				}
				modulestart1[0] = binfile[1300];
				modulestart1[1] = binfile[1301];
				modulestart1[2] = binfile[1302];
				modulestart1[3] = binfile[1303];
				moduleend1[0] = binfile[1304];
				moduleend1[1] = binfile[1305];
				moduleend1[2] = binfile[1306];
				moduleend1[3] = binfile[1307];
				modulestart2[0] = binfile[1308];
				modulestart2[1] = binfile[1309];
				modulestart2[2] = binfile[1310];
				modulestart2[3] = binfile[1311];
				moduleend2[0] = binfile[1312];
				moduleend2[1] = binfile[1313];
				moduleend2[2] = binfile[1314];
				moduleend2[3] = binfile[1315];
				modulestart3[0] = binfile[1316];
				modulestart3[1] = binfile[1317];
				modulestart3[2] = binfile[1318];
				modulestart3[3] = binfile[1319];
				moduleend3[0] = binfile[1320];
				moduleend3[1] = binfile[1321];
				moduleend3[2] = binfile[1322];
				moduleend3[3] = binfile[1323];
				modulestart4[0] = binfile[1324];
				modulestart4[1] = binfile[1325];
				modulestart4[2] = binfile[1326];
				modulestart4[3] = binfile[1327];
				moduleend4[0] = binfile[1328];
				moduleend4[1] = binfile[1329];
				moduleend4[2] = binfile[1330];
				moduleend4[3] = binfile[1331];
				modulestart5[0] = binfile[1332];
				modulestart5[1] = binfile[1333];
				modulestart5[2] = binfile[1334];
				modulestart5[3] = binfile[1335];
				moduleend5[0] = binfile[1336];
				moduleend5[1] = binfile[1337];
				moduleend5[2] = binfile[1338];
				moduleend5[3] = binfile[1339];
				modulestart6[0] = binfile[1340];
				modulestart6[1] = binfile[1341];
				modulestart6[2] = binfile[1342];
				modulestart6[3] = binfile[1343];
				moduleend6[0] = binfile[1344];
				moduleend6[1] = binfile[1345];
				moduleend6[2] = binfile[1346];
				moduleend6[3] = binfile[1347];
				modulestart7[0] = binfile[1348];
				modulestart7[1] = binfile[1349];
				modulestart7[2] = binfile[1350];
				modulestart7[3] = binfile[1351];
				moduleend7[0] = binfile[1352];
				moduleend7[1] = binfile[1353];
				moduleend7[2] = binfile[1354];
				moduleend7[3] = binfile[1355];
				x = ByteArrayToInt32(modulestart1);
				y = ByteArrayToInt32(moduleend1);
				modulelength1 = y - x + 1;
				x = ByteArrayToInt32(modulestart2);
				y = ByteArrayToInt32(moduleend2);
				modulelength2 = y - x + 1;
				x = ByteArrayToInt32(modulestart3);
				y = ByteArrayToInt32(moduleend3);
				modulelength3 = y - x + 1;
				x = ByteArrayToInt32(modulestart4);
				y = ByteArrayToInt32(moduleend4);
				modulelength4 = y - x + 1;
				x = ByteArrayToInt32(modulestart5);
				y = ByteArrayToInt32(moduleend5);
				modulelength5 = y - x + 1;
				x = ByteArrayToInt32(modulestart6);
				y = ByteArrayToInt32(moduleend6);
				modulelength6 = y - x + 1;
				x = ByteArrayToInt32(modulestart7);
				y = ByteArrayToInt32(moduleend7);
				modulelength7 = y - x + 1;
				if ((comboBoxFlashType.Text != "512KB A") & (comboBoxFlashType.Text != "512KB B") & (comboBoxFlashType.Text != "1024KB A") & (comboBoxFlashType.Text != "1024KB B"))
				{
					MessageBox.Show("PCM Type Unknown \nSelect PCM Type Manually \nor\nClick pcm Details", "PCM Type Unknown", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					cancel = 1;
				}
			}
		}

		private void GETOSFILEDATA(object sender, EventArgs e)
		{
			if (comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B")
			{
				if (length != 524288)
				{
					MessageBox.Show(this, "Incorrect Bin File Size For 512 KB Pcm . ", "Bin File Wrong , Not 512KB ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					cancel = 1;
					return;
				}
				x = 0;
				for (y = 0; x < 16384; x++)
				{
					osfile512[x] = binfile[y];
					y++;
				}
				for (y = 131072; x < 409600; x++)
				{
					osfile512[x] = binfile[y];
					y++;
				}
			}
			if (!(comboBoxFlashType.Text == "1024KB A") && !(comboBoxFlashType.Text == "1024KB B"))
			{
				return;
			}
			if (length != 1048576)
			{
				MessageBox.Show(this, "Incorrect Bin File Size For 1024 KB Pcm . ", "Bin File Wrong , Not 1024KB ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				cancel = 1;
				return;
			}
			x = 0;
			for (y = 0; x < 16384; x++)
			{
				osfile1024[x] = binfile[y];
				y++;
			}
			for (y = 131072; x < 933888; x++)
			{
				osfile1024[x] = binfile[y];
				y++;
			}
		}

		private void GETSECURITYFILEDATA(object sender, EventArgs e)
		{
			if (comboBoxFlashType.Text == "512KB A")
			{
				securitywritebytes[0] = 0;
				securitywritebytes[1] = 92;
				securitywritebytes[2] = 108;
				securitywritebytes[3] = 16;
				securitywritebytes[4] = 240;
				securitywritebytes[5] = 54;
				securitywritebytes[6] = 0;
				securitywritebytes[7] = 0;
				securitywritebytes[8] = 80;
				securitywritebytes[9] = byte.MaxValue;
				securitywritebytes[10] = 131;
				securitywritebytes[11] = 112;
				x = 12;
				for (y = 16384; x < 92; x++)
				{
					securitywritebytes[x] = binfile[y];
					y++;
				}
				if ((securitywritebytes[12] == byte.MaxValue) & (securitywritebytes[13] == byte.MaxValue) & (securitywritebytes[14] == byte.MaxValue) & (securitywritebytes[15] == byte.MaxValue) & (securitywritebytes[16] == byte.MaxValue) & (securitywritebytes[17] == byte.MaxValue) & (securitywritebytes[18] == byte.MaxValue) & (securitywritebytes[19] == byte.MaxValue) & (securitywritebytes[20] == byte.MaxValue))
				{
					x = 12;
					for (y = 24576; x < 92; x++)
					{
						securitywritebytes[x] = binfile[y];
						y++;
					}
				}
			}
			if (comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B" || comboBoxFlashType.Text == "512KB B")
			{
				securitywritebytes[0] = 0;
				securitywritebytes[1] = 92;
				securitywritebytes[2] = 108;
				securitywritebytes[3] = 16;
				securitywritebytes[4] = 240;
				securitywritebytes[5] = 54;
				securitywritebytes[6] = 0;
				securitywritebytes[7] = 0;
				securitywritebytes[8] = 80;
				securitywritebytes[9] = byte.MaxValue;
				securitywritebytes[10] = 128;
				securitywritebytes[11] = 0;
				x = 12;
				for (y = 16384; x < 92; x++)
				{
					securitywritebytes[x] = binfile[y];
					y++;
				}
				if ((securitywritebytes[12] == byte.MaxValue) & (securitywritebytes[13] == byte.MaxValue) & (securitywritebytes[14] == byte.MaxValue) & (securitywritebytes[15] == byte.MaxValue) & (securitywritebytes[16] == byte.MaxValue) & (securitywritebytes[17] == byte.MaxValue) & (securitywritebytes[18] == byte.MaxValue) & (securitywritebytes[19] == byte.MaxValue) & (securitywritebytes[20] == byte.MaxValue))
				{
					x = 12;
					for (y = 24576; x < 92; x++)
					{
						securitywritebytes[x] = binfile[y];
						y++;
					}
				}
			}
			if ((comboBoxFlashType.Text != "512KB A") & (comboBoxFlashType.Text != "512KB B") & (comboBoxFlashType.Text != "1024KB A") & (comboBoxFlashType.Text != "1024KB B"))
			{
				MessageBox.Show(this, "PCM Type Unknown , Select PCM Type", "PCM Type Unknown", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				cancel = 1;
			}
		}

		private void GETSERIALNUMBERDATA(object sender, EventArgs e)
		{
			if (comboBoxFlashType.Text == "512KB A")
			{
				serialnowritebytes[0] = 0;
				serialnowritebytes[1] = 24;
				serialnowritebytes[2] = 108;
				serialnowritebytes[3] = 16;
				serialnowritebytes[4] = 240;
				serialnowritebytes[5] = 54;
				serialnowritebytes[6] = 0;
				serialnowritebytes[7] = 0;
				serialnowritebytes[8] = 12;
				serialnowritebytes[9] = byte.MaxValue;
				serialnowritebytes[10] = 131;
				serialnowritebytes[11] = 120;
				buffer0 = textBoxSerialNumber.Text;
				serialnowritebytes[12] = Convert.ToByte(buffer0[0]);
				serialnowritebytes[13] = Convert.ToByte(buffer0[1]);
				serialnowritebytes[14] = Convert.ToByte(buffer0[2]);
				serialnowritebytes[15] = Convert.ToByte(buffer0[3]);
				serialnowritebytes[16] = Convert.ToByte(buffer0[4]);
				serialnowritebytes[17] = Convert.ToByte(buffer0[5]);
				serialnowritebytes[18] = Convert.ToByte(buffer0[6]);
				serialnowritebytes[19] = Convert.ToByte(buffer0[7]);
				serialnowritebytes[20] = Convert.ToByte(buffer0[8]);
				serialnowritebytes[21] = Convert.ToByte(buffer0[9]);
				serialnowritebytes[22] = Convert.ToByte(buffer0[10]);
				serialnowritebytes[23] = Convert.ToByte(buffer0[11]);
			}
			if (comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B" || comboBoxFlashType.Text == "512KB B")
			{
				serialnowritebytes[0] = 0;
				serialnowritebytes[1] = 24;
				serialnowritebytes[2] = 108;
				serialnowritebytes[3] = 16;
				serialnowritebytes[4] = 240;
				serialnowritebytes[5] = 54;
				serialnowritebytes[6] = 0;
				serialnowritebytes[7] = 0;
				serialnowritebytes[8] = 12;
				serialnowritebytes[9] = byte.MaxValue;
				serialnowritebytes[10] = 128;
				serialnowritebytes[11] = 8;
				buffer0 = textBoxSerialNumber.Text;
				serialnowritebytes[12] = Convert.ToByte(buffer0[0]);
				serialnowritebytes[13] = Convert.ToByte(buffer0[1]);
				serialnowritebytes[14] = Convert.ToByte(buffer0[2]);
				serialnowritebytes[15] = Convert.ToByte(buffer0[3]);
				serialnowritebytes[16] = Convert.ToByte(buffer0[4]);
				serialnowritebytes[17] = Convert.ToByte(buffer0[5]);
				serialnowritebytes[18] = Convert.ToByte(buffer0[6]);
				serialnowritebytes[19] = Convert.ToByte(buffer0[7]);
				serialnowritebytes[20] = Convert.ToByte(buffer0[8]);
				serialnowritebytes[21] = Convert.ToByte(buffer0[9]);
				serialnowritebytes[22] = Convert.ToByte(buffer0[10]);
				serialnowritebytes[23] = Convert.ToByte(buffer0[11]);
			}
		}

		private void CHECKOSCALMISMATCH(object sender, EventArgs e)
		{
			if (!((pcmosnumber[0] == binfileosnumber[0]) & (pcmosnumber[1] == binfileosnumber[1]) & (pcmosnumber[2] == binfileosnumber[2]) & (pcmosnumber[3] == binfileosnumber[3])))
			{
				switch (MessageBox.Show("PCM And Selected Bin File Have Different Operating System Numbers\nOnly Cick Yes If PCM Is In Program Prompt Mode \n\nContinue ? ", "PCM / Bin File OS Mismatch", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.No:
					cancel = 1;
					MessageBox.Show("Calibration File Write Aborted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				}
			}
		}

		private void SETCABLESPEED1X(object sender, EventArgs e)
		{
			if (serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				SerialPort serialPort = serialPort1;
				byte[] buffer = new byte[2];
				serialPort.Write(buffer, 0, 2);
			}
		}

		private void DISABLEPCMCHATTER(object sender, EventArgs e)
		{
			if (cancel != 0)
			{
				return;
			}
			vpwmessagerx5[3] = 0;
			if (serialPort1.IsOpen)
			{
				textBox1.AppendText("Disable Chatter");
				textBox1.AppendText("\n");
				vpwmessagerx6[3] = 0;
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					254,
					240,
					40,
					0
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW5BYTERESPONCE));
				if (vpwmessagerx5[3] != 104)
				{
					textBox1.AppendText("Disable Chatter Fail");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				if (vpwmessagerx5[3] != 104)
				{
					Thread.Sleep(100);
					textBox1.AppendText("Disable Chatter Retry");
					textBox1.AppendText("\n");
					vpwmessagerx6[3] = 0;
					serialPort1.DiscardInBuffer();
					serialPort1.Write(new byte[7]
					{
						0,
						5,
						108,
						254,
						240,
						40,
						0
					}, 0, 7);
					Invoke(new EventHandler(LISTENFORVPW5BYTERESPONCE));
				}
				if (vpwmessagerx5[3] != 104)
				{
					textBox1.AppendText("Disable Chatter Retry Fail");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
					cancel = 1;
				}
				if (vpwmessagerx5[3] == 104)
				{
					textBox1.AppendText("Disable Chatter Success");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
			}
		}

		private void ENABLEPCMCHATTER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				textBox1.AppendText("Enable Chatter");
				textBox1.AppendText("\n");
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[6]
				{
					0,
					4,
					108,
					254,
					240,
					41
				}, 0, 6);
				Invoke(new EventHandler(LISTENFORVPW5BYTERESPONCE));
			}
		}

		private void ENABLEPCMCHATTERHISPD(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				textBox1.AppendText("Enable Chatter");
				textBox1.AppendText("\n");
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[6]
				{
					128,
					4,
					108,
					254,
					240,
					32
				}, 0, 6);
				Invoke(new EventHandler(LISTENFORVPW5BYTERESPONCE));
			}
		}

		private void READALLPCMCODES(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				textBox1.AppendText("Reading PCM Fault Codes");
				textBox1.AppendText("\n");
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[9]
				{
					0,
					7,
					108,
					16,
					241,
					25,
					218,
					255,
					0
				}, 0, 9);
				Invoke(new EventHandler(LISTENFORFAULTCODERESPONCE));
				textBox1.AppendText("\n");
			}
		}

		private void CLEARPCMCODES(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				textBox1.AppendText("Clearing PCM Fault Codes");
				textBox1.AppendText("\n");
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[6]
				{
					0,
					4,
					108,
					16,
					241,
					20
				}, 0, 6);
				Invoke(new EventHandler(LISTENFORVPW5BYTERESPONCE));
				if (vpwmessagerx5[3] == 84)
				{
					textBox1.AppendText("All PCM Codes Cleared");
					textBox1.AppendText("\n");
				}
				textBox1.AppendText("\n");
			}
		}

		private void REQUESTMODULE0NUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					10
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				pcmosnumber[0] = vpwmessagerx10[5];
				pcmosnumber[1] = vpwmessagerx10[6];
				pcmosnumber[2] = vpwmessagerx10[7];
				pcmosnumber[3] = vpwmessagerx10[8];
				x = vpwmessagerx10[5] * 16777216 + vpwmessagerx10[6] * 65536 + vpwmessagerx10[7] * 256 + vpwmessagerx10[8];
				textBoxModule0.Clear();
				textBoxModule0.Text = Convert.ToString(x);
			}
		}

		private void REQUESTMODULE1NUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					11
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				x = vpwmessagerx10[5] * 16777216 + vpwmessagerx10[6] * 65536 + vpwmessagerx10[7] * 256 + vpwmessagerx10[8];
				textBoxModule1.Clear();
				textBoxModule1.Text = Convert.ToString(x);
			}
		}

		private void REQUESTMODULE2NUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					12
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				x = vpwmessagerx10[5] * 16777216 + vpwmessagerx10[6] * 65536 + vpwmessagerx10[7] * 256 + vpwmessagerx10[8];
				textBoxModule2.Clear();
				textBoxModule2.Text = Convert.ToString(x);
			}
		}

		private void REQUESTMODULE3NUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					13
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				x = vpwmessagerx10[5] * 16777216 + vpwmessagerx10[6] * 65536 + vpwmessagerx10[7] * 256 + vpwmessagerx10[8];
				textBoxModule3.Clear();
				textBoxModule3.Text = Convert.ToString(x);
			}
		}

		private void REQUESTMODULE4NUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					14
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				x = vpwmessagerx10[5] * 16777216 + vpwmessagerx10[6] * 65536 + vpwmessagerx10[7] * 256 + vpwmessagerx10[8];
				textBoxModule4.Clear();
				textBoxModule4.Text = Convert.ToString(x);
			}
		}

		private void REQUESTMODULE5NUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					15
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				x = vpwmessagerx10[5] * 16777216 + vpwmessagerx10[6] * 65536 + vpwmessagerx10[7] * 256 + vpwmessagerx10[8];
				textBoxModule5.Clear();
				textBoxModule5.Text = Convert.ToString(x);
			}
		}

		private void REQUESTMODULE6NUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					16
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				x = vpwmessagerx10[5] * 16777216 + vpwmessagerx10[6] * 65536 + vpwmessagerx10[7] * 256 + vpwmessagerx10[8];
				textBoxModule6.Clear();
				textBoxModule6.Text = Convert.ToString(x);
			}
		}

		private void REQUESTMODULE7NUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					17
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				x = vpwmessagerx10[5] * 16777216 + vpwmessagerx10[6] * 65536 + vpwmessagerx10[7] * 256 + vpwmessagerx10[8];
				textBoxModule7.Clear();
				textBoxModule7.Text = Convert.ToString(x);
			}
		}

		private void REQUESTHARDWARENUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					4
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				x = vpwmessagerx10[5] * 16777216 + vpwmessagerx10[6] * 65536 + vpwmessagerx10[7] * 256 + vpwmessagerx10[8];
				textBoxPCMHardwareNo.Clear();
				textBoxPCMHardwareNo.Text = Convert.ToString(x);
				if (x == 9386530)
				{
					comboBoxFlashType.Text = "512KB B";
				}
				else if (x == 16220610)
				{
					comboBoxFlashType.Text = "512KB A";
				}
				else if (x == 12583659)
				{
					comboBoxFlashType.Text = "1024KB A";
				}
				else
				{
					comboBoxFlashType.Text = "Unknown";
				}
			}
		}

		private void REQUESTVINNUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					1
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW12BYTERESPONCE));
				textBoxVIN.Clear();
				vin[0] = Convert.ToChar(vpwmessagerx[6]);
				vin[1] = Convert.ToChar(vpwmessagerx[7]);
				vin[2] = Convert.ToChar(vpwmessagerx[8]);
				vin[3] = Convert.ToChar(vpwmessagerx[9]);
				vin[4] = Convert.ToChar(vpwmessagerx[10]);
				Thread.Sleep(10);
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					2
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW12BYTERESPONCE));
				vin[5] = Convert.ToChar(vpwmessagerx[5]);
				vin[6] = Convert.ToChar(vpwmessagerx[6]);
				vin[7] = Convert.ToChar(vpwmessagerx[7]);
				vin[8] = Convert.ToChar(vpwmessagerx[8]);
				vin[9] = Convert.ToChar(vpwmessagerx[9]);
				vin[10] = Convert.ToChar(vpwmessagerx[10]);
				Thread.Sleep(10);
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					3
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW12BYTERESPONCE));
				vin[11] = Convert.ToChar(vpwmessagerx[5]);
				vin[12] = Convert.ToChar(vpwmessagerx[6]);
				vin[13] = Convert.ToChar(vpwmessagerx[7]);
				vin[14] = Convert.ToChar(vpwmessagerx[8]);
				vin[15] = Convert.ToChar(vpwmessagerx[9]);
				vin[16] = Convert.ToChar(vpwmessagerx[10]);
				Thread.Sleep(10);
				char[] array = vin;
				foreach (char value in array)
				{
					textBoxVIN.AppendText(Convert.ToString(value));
				}
			}
		}

		private void REQUESTSERIALNUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					5
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				textBoxSerialNumber.Clear();
				serialno[0] = Convert.ToChar(vpwmessagerx10[5]);
				serialno[1] = Convert.ToChar(vpwmessagerx10[6]);
				serialno[2] = Convert.ToChar(vpwmessagerx10[7]);
				serialno[3] = Convert.ToChar(vpwmessagerx10[8]);
				Thread.Sleep(10);
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					6
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				serialno[4] = Convert.ToChar(vpwmessagerx10[5]);
				serialno[5] = Convert.ToChar(vpwmessagerx10[6]);
				serialno[6] = Convert.ToChar(vpwmessagerx10[7]);
				serialno[7] = Convert.ToChar(vpwmessagerx10[8]);
				Thread.Sleep(10);
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					7
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				serialno[8] = Convert.ToChar(vpwmessagerx10[5]);
				serialno[9] = Convert.ToChar(vpwmessagerx10[6]);
				serialno[10] = Convert.ToChar(vpwmessagerx10[7]);
				serialno[11] = Convert.ToChar(vpwmessagerx10[8]);
				Thread.Sleep(10);
				char[] array = serialno;
				foreach (char value in array)
				{
					textBoxSerialNumber.AppendText(Convert.ToString(value));
				}
			}
		}

		private void REQUESTBCCNUMBER(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					60,
					20
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW10BYTERESPONCE));
				textBoxBCC.Clear();
				bcc[0] = Convert.ToChar(vpwmessagerx10[5]);
				bcc[1] = Convert.ToChar(vpwmessagerx10[6]);
				bcc[2] = Convert.ToChar(vpwmessagerx10[7]);
				bcc[3] = Convert.ToChar(vpwmessagerx10[8]);
				Thread.Sleep(10);
				char[] array = bcc;
				foreach (char value in array)
				{
					textBoxBCC.AppendText(Convert.ToString(value));
				}
			}
		}

		private void REQUESTSEED(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					39,
					1
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORPCMSEEDRESPONCE));
				seed[0] = vpwmessagerx8[5];
				seed[1] = vpwmessagerx8[6];
				buffer0 = ByteArrayToHexString(seed);
				textBoxSeed.Clear();
				textBoxSeed.AppendText(buffer0);
				a = seed[1] * 256 + seed[0];
				if (a > 37709)
				{
					a = 103245 - a;
				}
				if (a <= 37709)
				{
					a = 37709 - a;
				}
				for (b = 0; a > 255; a -= 256)
				{
					b++;
				}
				key[0] = Convert.ToByte(b);
				key[1] = Convert.ToByte(a);
				buffer0 = ByteArrayToHexString(key);
				textBoxKey.Clear();
				textBoxKey.AppendText(buffer0);
			}
		}

		private void UNLOCKPCM(object sender, EventArgs e)
		{
			if (cancel != 0)
			{
				return;
			}
			textBox1.AppendText("Unlocking PCM");
			textBox1.AppendText("\n");
			if (!serialPort1.IsOpen)
			{
				return;
			}
			serialPort1.DiscardInBuffer();
			serialPort1.Write(new byte[7]
			{
				0,
				5,
				108,
				16,
				240,
				39,
				1
			}, 0, 7);
			Invoke(new EventHandler(LISTENFORPCMSEEDRESPONCE));
			seed[0] = vpwmessagerx8[5];
			seed[1] = vpwmessagerx8[6];
			buffer0 = ByteArrayToHexString(seed);
			textBoxSeed.Clear();
			textBoxSeed.AppendText(buffer0);
			key = HexStringToByteArray(textBoxKey.Text);
			if (cancel == 0)
			{
				Thread.Sleep(10);
				serialPort1.DiscardInBuffer();
				SerialPort serialPort = serialPort1;
				byte[] array = new byte[9]
				{
					0,
					7,
					108,
					16,
					240,
					39,
					2,
					0,
					0
				};
				array[7] = key[0];
				array[8] = key[1];
				serialPort.Write(array, 0, 9);
				Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
				if (vpwmessagerx7[5] == 52)
				{
					textBox1.AppendText("PCM Unlock Success");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				if (vpwmessagerx7[5] == 53)
				{
					MessageBox.Show(this, "Could Not Unlock PCM. , Incorrect Unlock Key. ", "Check Key ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					cancel = 1;
				}
			}
		}

		private void SWITCHPCMTOHISPEED4X(object sender, EventArgs e)
		{
			if (cancel != 0)
			{
				return;
			}
			textBox1.AppendText("Request High Speed");
			textBox1.AppendText("\n");
			if (serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[6]
				{
					0,
					4,
					108,
					254,
					240,
					160
				}, 0, 6);
				Invoke(new EventHandler(LISTENFORVPW6BYTERESPONCE));
				if (vpwmessagerx6[4] == 170)
				{
					textBox1.AppendText("Hi Speed Success");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				if (vpwmessagerx6[4] != 170)
				{
					textBox1.AppendText("Hi Speed Fail");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
					cancel = 1;
				}
				Thread.Sleep(100);
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[6]
				{
					0,
					4,
					108,
					254,
					240,
					161
				}, 0, 6);
				Thread.Sleep(100);
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[6]
				{
					128,
					4,
					140,
					254,
					240,
					63
				}, 0, 6);
			}
		}

		private void REQUESTMODE34A(object sender, EventArgs e)
		{
			if (cancel == 0)
			{
				textBox1.AppendText("Request Data Transfer");
				textBox1.AppendText("\n");
				if (serialPort1.IsOpen)
				{
					serialPort1.DiscardInBuffer();
					serialPort1.Write(new byte[12]
					{
						128,
						10,
						108,
						16,
						240,
						52,
						0,
						3,
						50,
						255,
						145,
						62
					}, 0, 12);
					Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
				}
			}
		}

		private void REQUESTMODE34B(object sender, EventArgs e)
		{
			if (cancel == 0)
			{
				textBox1.AppendText("Request Data Transfer");
				textBox1.AppendText("\n");
				if (serialPort1.IsOpen)
				{
					serialPort1.DiscardInBuffer();
					serialPort1.Write(new byte[12]
					{
						128,
						10,
						108,
						16,
						240,
						52,
						0,
						15,
						66,
						255,
						129,
						238
					}, 0, 12);
					Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
				}
			}
		}

		private void REQUESTMODE34C(object sender, EventArgs e)
		{
			if (cancel == 0)
			{
				textBox1.AppendText("Request Data Transfer");
				textBox1.AppendText("\n");
				if (serialPort1.IsOpen)
				{
					serialPort1.DiscardInBuffer();
					serialPort1.Write(new byte[12]
					{
						128,
						10,
						108,
						16,
						240,
						52,
						0,
						4,
						0,
						255,
						129,
						254
					}, 0, 12);
					Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
				}
			}
		}

		private void REQUESTMODE34D(object sender, EventArgs e)
		{
			if (cancel == 0)
			{
				textBox1.AppendText("Request Data Transfer");
				textBox1.AppendText("\n");
				if (serialPort1.IsOpen)
				{
					serialPort1.DiscardInBuffer();
					serialPort1.Write(new byte[12]
					{
						0,
						10,
						108,
						16,
						240,
						52,
						0,
						0,
						80,
						255,
						128,
						0
					}, 0, 12);
					Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
				}
			}
		}

		private void REQUESTMODE34E(object sender, EventArgs e)
		{
			if (cancel == 0)
			{
				textBox1.AppendText("Request Data Transfer");
				textBox1.AppendText("\n");
				if (serialPort1.IsOpen)
				{
					serialPort1.DiscardInBuffer();
					serialPort1.Write(new byte[12]
					{
						0,
						10,
						108,
						16,
						240,
						52,
						0,
						0,
						4,
						255,
						128,
						0
					}, 0, 12);
					Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
				}
			}
		}

		private void TESTERPRESENTMESSAGE(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.Write(new byte[6]
				{
					128,
					4,
					140,
					254,
					240,
					63
				}, 0, 6);
			}
		}

		private void TESTERPRESENTMESSLOSPEED(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.Write(new byte[6]
				{
					0,
					4,
					140,
					254,
					240,
					63
				}, 0, 6);
			}
		}

		private void WRITEBOOTLOADREAD(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[864]
				{
					131,
					94,
					109,
					16,
					240,
					54,
					0,
					3,
					82,
					255,
					145,
					62,
					97,
					0,
					1,
					16,
					19,
					252,
					0,
					3,
					0,
					255,
					246,
					12,
					19,
					252,
					0,
					0,
					0,
					255,
					246,
					13,
					97,
					0,
					0,
					164,
					97,
					0,
					0,
					248,
					16,
					57,
					0,
					255,
					246,
					14,
					2,
					0,
					0,
					224,
					12,
					0,
					0,
					224,
					102,
					232,
					16,
					57,
					0,
					255,
					246,
					15,
					97,
					0,
					0,
					168,
					97,
					0,
					2,
					50,
					12,
					57,
					0,
					32,
					0,
					255,
					148,
					39,
					102,
					0,
					0,
					42,
					32,
					124,
					0,
					255,
					148,
					64,
					32,
					57,
					0,
					255,
					148,
					68,
					97,
					0,
					0,
					200,
					19,
					252,
					0,
					64,
					0,
					255,
					246,
					12,
					19,
					252,
					0,
					0,
					0,
					255,
					246,
					13,
					97,
					0,
					0,
					116,
					78,
					112,
					96,
					254,
					12,
					57,
					0,
					53,
					0,
					255,
					148,
					39,
					102,
					190,
					32,
					124,
					0,
					255,
					148,
					84,
					32,
					57,
					0,
					255,
					148,
					92,
					97,
					0,
					0,
					150,
					66,
					128,
					128,
					57,
					0,
					255,
					148,
					43,
					225,
					136,
					128,
					57,
					0,
					255,
					148,
					44,
					225,
					136,
					128,
					57,
					0,
					255,
					148,
					45,
					32,
					64,
					66,
					128,
					128,
					57,
					0,
					255,
					148,
					41,
					225,
					136,
					128,
					57,
					0,
					255,
					148,
					42,
					97,
					0,
					0,
					190,
					96,
					0,
					255,
					126,
					19,
					252,
					0,
					85,
					0,
					255,
					250,
					39,
					19,
					252,
					0,
					170,
					0,
					255,
					250,
					39,
					8,
					185,
					0,
					7,
					0,
					255,
					208,
					6,
					8,
					249,
					0,
					7,
					0,
					255,
					208,
					6,
					78,
					117,
					42,
					60,
					0,
					0,
					39,
					16,
					97,
					214,
					97,
					0,
					0,
					44,
					97,
					0,
					0,
					40,
					97,
					0,
					0,
					36,
					97,
					0,
					0,
					32,
					97,
					0,
					0,
					28,
					97,
					0,
					0,
					24,
					97,
					0,
					0,
					20,
					97,
					0,
					0,
					16,
					97,
					0,
					0,
					12,
					97,
					0,
					0,
					8,
					81,
					205,
					255,
					212,
					78,
					117,
					78,
					113,
					78,
					113,
					78,
					113,
					78,
					113,
					78,
					117,
					85,
					64,
					97,
					154,
					19,
					252,
					0,
					20,
					0,
					255,
					246,
					12,
					19,
					216,
					0,
					255,
					246,
					13,
					97,
					226,
					81,
					200,
					255,
					246,
					19,
					252,
					0,
					12,
					0,
					255,
					246,
					12,
					19,
					216,
					0,
					255,
					246,
					13,
					97,
					206,
					19,
					252,
					0,
					3,
					0,
					255,
					246,
					12,
					19,
					252,
					0,
					0,
					0,
					255,
					246,
					13,
					97,
					0,
					255,
					100,
					97,
					184,
					16,
					57,
					0,
					255,
					246,
					14,
					2,
					0,
					0,
					224,
					12,
					0,
					0,
					224,
					102,
					234,
					16,
					57,
					0,
					255,
					246,
					15,
					78,
					117,
					97,
					0,
					255,
					70,
					34,
					8,
					19,
					193,
					0,
					255,
					148,
					105,
					224,
					137,
					19,
					193,
					0,
					255,
					148,
					104,
					224,
					137,
					19,
					193,
					0,
					255,
					148,
					103,
					34,
					0,
					19,
					193,
					0,
					255,
					148,
					102,
					224,
					137,
					19,
					193,
					0,
					255,
					148,
					101,
					114,
					9,
					34,
					124,
					0,
					255,
					148,
					96,
					19,
					252,
					0,
					20,
					0,
					255,
					246,
					12,
					19,
					217,
					0,
					255,
					246,
					13,
					97,
					0,
					255,
					4,
					81,
					201,
					255,
					244,
					97,
					0,
					254,
					252,
					83,
					128,
					66,
					129,
					66,
					130,
					20,
					57,
					0,
					255,
					148,
					100,
					210,
					130,
					20,
					57,
					0,
					255,
					148,
					101,
					210,
					130,
					20,
					57,
					0,
					255,
					148,
					102,
					210,
					130,
					20,
					57,
					0,
					255,
					148,
					103,
					210,
					130,
					20,
					57,
					0,
					255,
					148,
					104,
					210,
					130,
					20,
					57,
					0,
					255,
					148,
					105,
					210,
					130,
					97,
					0,
					254,
					194,
					20,
					57,
					0,
					255,
					246,
					14,
					2,
					2,
					0,
					3,
					12,
					2,
					0,
					3,
					103,
					236,
					19,
					252,
					0,
					4,
					0,
					255,
					246,
					12,
					19,
					208,
					0,
					255,
					246,
					13,
					20,
					24,
					210,
					130,
					81,
					200,
					255,
					216,
					32,
					1,
					224,
					136,
					97,
					0,
					254,
					148,
					20,
					57,
					0,
					255,
					246,
					14,
					2,
					2,
					0,
					3,
					12,
					2,
					0,
					3,
					103,
					236,
					19,
					252,
					0,
					4,
					0,
					255,
					246,
					12,
					19,
					192,
					0,
					255,
					246,
					13,
					97,
					0,
					254,
					114,
					20,
					57,
					0,
					255,
					246,
					14,
					2,
					2,
					0,
					3,
					12,
					2,
					0,
					3,
					103,
					236,
					19,
					252,
					0,
					12,
					0,
					255,
					246,
					12,
					19,
					193,
					0,
					255,
					246,
					13,
					78,
					117,
					66,
					134,
					97,
					0,
					254,
					76,
					16,
					57,
					0,
					255,
					246,
					14,
					2,
					0,
					0,
					224,
					12,
					0,
					0,
					64,
					103,
					0,
					0,
					16,
					82,
					134,
					12,
					134,
					0,
					38,
					37,
					160,
					103,
					0,
					253,
					202,
					96,
					220,
					32,
					124,
					0,
					255,
					148,
					36,
					16,
					249,
					0,
					255,
					246,
					15,
					97,
					0,
					254,
					28,
					16,
					57,
					0,
					255,
					246,
					14,
					2,
					0,
					0,
					224,
					12,
					0,
					0,
					64,
					102,
					0,
					0,
					16,
					82,
					134,
					12,
					134,
					0,
					38,
					37,
					160,
					103,
					0,
					253,
					154,
					96,
					214,
					16,
					249,
					0,
					255,
					246,
					15,
					32,
					124,
					0,
					255,
					148,
					36,
					12,
					16,
					0,
					108,
					102,
					154,
					12,
					40,
					0,
					16,
					0,
					1,
					103,
					0,
					0,
					10,
					12,
					40,
					0,
					254,
					0,
					1,
					102,
					136,
					78,
					117,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					109,
					240,
					16,
					12,
					161,
					0,
					0,
					0,
					0,
					0,
					0,
					5,
					109,
					240,
					16,
					96,
					0,
					0,
					0,
					4,
					109,
					240,
					16,
					118,
					0,
					115,
					0,
					0,
					0,
					0,
					0,
					6,
					109,
					240,
					16,
					117,
					0,
					115,
					0,
					0,
					0,
					0,
					0,
					6,
					109,
					240,
					16,
					54,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					10,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					28,
					25
				}, 0, 864);
				Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
			}
		}

		private void WRITE1024WRITEBOOTLOADA(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[1038]
				{
					132,
					12,
					109,
					16,
					240,
					54,
					0,
					4,
					0,
					255,
					129,
					254,
					0,
					124,
					7,
					0,
					157,
					206,
					46,
					124,
					0,
					255,
					184,
					0,
					74,
					121,
					0,
					255,
					130,
					244,
					103,
					6,
					78,
					185,
					0,
					255,
					141,
					254,
					36,
					121,
					0,
					255,
					130,
					240,
					78,
					146,
					78,
					114,
					39,
					0,
					16,
					16,
					32,
					32,
					64,
					64,
					80,
					80,
					112,
					112,
					144,
					144,
					176,
					176,
					208,
					208,
					255,
					255,
					128,
					128,
					160,
					160,
					176,
					176,
					48,
					48,
					240,
					240,
					0,
					0,
					10,
					170,
					0,
					0,
					5,
					84,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					1,
					170,
					170,
					85,
					85,
					144,
					144,
					0,
					0,
					128,
					0,
					0,
					2,
					0,
					0,
					0,
					4,
					0,
					0,
					0,
					6,
					0,
					0,
					0,
					8,
					0,
					0,
					0,
					10,
					0,
					0,
					0,
					12,
					0,
					0,
					0,
					14,
					0,
					0,
					0,
					0,
					128,
					0,
					0,
					1,
					0,
					0,
					0,
					2,
					0,
					0,
					0,
					3,
					0,
					0,
					0,
					4,
					0,
					0,
					0,
					5,
					0,
					0,
					0,
					6,
					0,
					0,
					0,
					7,
					0,
					0,
					0,
					8,
					0,
					0,
					0,
					9,
					0,
					0,
					0,
					10,
					0,
					0,
					0,
					11,
					0,
					0,
					0,
					12,
					0,
					0,
					0,
					13,
					0,
					0,
					0,
					14,
					0,
					0,
					0,
					15,
					0,
					0,
					0,
					1,
					255,
					255,
					0,
					15,
					255,
					255,
					115,
					131,
					132,
					133,
					133,
					134,
					135,
					136,
					137,
					138,
					104,
					32,
					112,
					96,
					16,
					96,
					0,
					7,
					0,
					133,
					8,
					133,
					112,
					0,
					64,
					0,
					3,
					0,
					224,
					0,
					3,
					255,
					0,
					0,
					0,
					0,
					128,
					106,
					1,
					0,
					0,
					1,
					0,
					255,
					184,
					0,
					0,
					255,
					129,
					254,
					0,
					255,
					131,
					26,
					0,
					0,
					12,
					20,
					28,
					255,
					32,
					23,
					78,
					114,
					39,
					0,
					12,
					64,
					0,
					2,
					99,
					18,
					83,
					64,
					78,
					113,
					78,
					113,
					78,
					113,
					78,
					113,
					78,
					113,
					78,
					113,
					83,
					64,
					102,
					240,
					78,
					117,
					64,
					231,
					0,
					124,
					7,
					0,
					78,
					185,
					0,
					255,
					145,
					94,
					78,
					185,
					0,
					255,
					138,
					122,
					70,
					223,
					78,
					117,
					78,
					86,
					255,
					254,
					61,
					121,
					0,
					255,
					130,
					62,
					255,
					254,
					36,
					121,
					0,
					255,
					130,
					64,
					52,
					174,
					255,
					254,
					78,
					94,
					78,
					117,
					38,
					121,
					0,
					255,
					130,
					64,
					36,
					121,
					0,
					255,
					130,
					68,
					54,
					185,
					0,
					255,
					130,
					80,
					52,
					185,
					0,
					255,
					130,
					82,
					78,
					117,
					47,
					0,
					78,
					185,
					0,
					255,
					141,
					164,
					78,
					185,
					0,
					255,
					141,
					216,
					12,
					64,
					1,
					152,
					101,
					6,
					12,
					64,
					2,
					131,
					99,
					12,
					16,
					57,
					0,
					255,
					130,
					191,
					78,
					185,
					0,
					255,
					140,
					252,
					32,
					31,
					78,
					117,
					47,
					0,
					78,
					185,
					0,
					255,
					141,
					44,
					48,
					60,
					39,
					22,
					78,
					185,
					0,
					255,
					131,
					0,
					78,
					185,
					0,
					255,
					141,
					44,
					78,
					185,
					0,
					255,
					141,
					150,
					74,
					0,
					102,
					12,
					16,
					57,
					0,
					255,
					130,
					196,
					78,
					185,
					0,
					255,
					140,
					252,
					32,
					31,
					78,
					117,
					78,
					86,
					255,
					252,
					72,
					231,
					227,
					192,
					66,
					56,
					176,
					48,
					116,
					1,
					17,
					194,
					176,
					16,
					74,
					56,
					176,
					16,
					103,
					10,
					66,
					56,
					176,
					16,
					78,
					185,
					0,
					255,
					137,
					84,
					32,
					56,
					176,
					18,
					12,
					128,
					0,
					0,
					54,
					176,
					99,
					6,
					116,
					1,
					17,
					194,
					176,
					59,
					78,
					185,
					0,
					255,
					148,
					44,
					74,
					56,
					176,
					16,
					102,
					0,
					2,
					248,
					74,
					128,
					102,
					98,
					66,
					120,
					176,
					54,
					78,
					185,
					0,
					255,
					137,
					132,
					74,
					56,
					176,
					16,
					102,
					82,
					12,
					56,
					0,
					2,
					176,
					34,
					101,
					56,
					12,
					120,
					74,
					252,
					176,
					144,
					103,
					14,
					16,
					57,
					0,
					255,
					130,
					199,
					78,
					185,
					0,
					255,
					140,
					252,
					96,
					52,
					112,
					1,
					17,
					192,
					176,
					40,
					66,
					56,
					176,
					48,
					74,
					56,
					176,
					50,
					102,
					36,
					78,
					185,
					0,
					255,
					131,
					100,
					78,
					185,
					0,
					255,
					135,
					20,
					17,
					192,
					176,
					50,
					96,
					18,
					78,
					185,
					0,
					255,
					131,
					100,
					78,
					185,
					0,
					255,
					135,
					20,
					17,
					252,
					0,
					1,
					176,
					50,
					44,
					56,
					176,
					18,
					34,
					56,
					176,
					30,
					146,
					134,
					12,
					129,
					0,
					0,
					8,
					0,
					101,
					4,
					50,
					60,
					8,
					0,
					36,
					1,
					226,
					74,
					32,
					124,
					0,
					255,
					160,
					0,
					36,
					72,
					66,
					64,
					118,
					1,
					42,
					3,
					186,
					66,
					98,
					12,
					208,
					82,
					84,
					138,
					82,
					67,
					101,
					4,
					180,
					67,
					100,
					244,
					209,
					120,
					176,
					54,
					49,
					197,
					176,
					56,
					74,
					56,
					176,
					48,
					103,
					0,
					0,
					132,
					74,
					56,
					176,
					58,
					102,
					124,
					12,
					134,
					0,
					0,
					56,
					0,
					99,
					116,
					32,
					6,
					52,
					60,
					64,
					0,
					148,
					64,
					34,
					2,
					220,
					184,
					176,
					22,
					45,
					70,
					255,
					252,
					78,
					185,
					0,
					255,
					141,
					44,
					74,
					56,
					176,
					16,
					102,
					86,
					31,
					56,
					176,
					148,
					63,
					56,
					176,
					146,
					32,
					1,
					67,
					238,
					255,
					252,
					78,
					185,
					0,
					255,
					139,
					94,
					54,
					56,
					176,
					142,
					12,
					67,
					0,
					137,
					102,
					10,
					33,
					249,
					0,
					255,
					130,
					90,
					176,
					22,
					96,
					14,
					12,
					67,
					0,
					1,
					102,
					8,
					33,
					249,
					0,
					255,
					130,
					126,
					176,
					22,
					66,
					184,
					176,
					18,
					4,
					184,
					0,
					2,
					0,
					0,
					176,
					30,
					124,
					1,
					17,
					198,
					176,
					58,
					50,
					60,
					8,
					0,
					146,
					66,
					226,
					74,
					82,
					66,
					49,
					194,
					176,
					56,
					38,
					56,
					176,
					22,
					214,
					184,
					176,
					18,
					45,
					67,
					255,
					252,
					74,
					56,
					176,
					16,
					102,
					38,
					78,
					185,
					0,
					255,
					141,
					44,
					31,
					56,
					176,
					148,
					63,
					56,
					176,
					146,
					32,
					1,
					67,
					238,
					255,
					252,
					48,
					120,
					176,
					56,
					65,
					240,
					131,
					176,
					0,
					255,
					159,
					254,
					78,
					185,
					0,
					255,
					139,
					94,
					2,
					129,
					0,
					0,
					255,
					255,
					210,
					184,
					176,
					18,
					33,
					193,
					176,
					18,
					12,
					129,
					0,
					0,
					54,
					176,
					99,
					6,
					17,
					252,
					0,
					1,
					176,
					59,
					36,
					56,
					176,
					30,
					178,
					130,
					100,
					38,
					74,
					56,
					176,
					16,
					102,
					32,
					66,
					64,
					78,
					185,
					0,
					255,
					131,
					0,
					16,
					57,
					0,
					255,
					130,
					190,
					78,
					185,
					0,
					255,
					145,
					6,
					78,
					185,
					0,
					255,
					150,
					10,
					78,
					185,
					0,
					255,
					148,
					44,
					178,
					130,
					100,
					8,
					74,
					56,
					176,
					16,
					103,
					0,
					254,
					170,
					74,
					56,
					176,
					16,
					102,
					0,
					1,
					50,
					74,
					120,
					176,
					54,
					103,
					12,
					16,
					57,
					0,
					255,
					130,
					194,
					78,
					185,
					0,
					255,
					140,
					252,
					74,
					56,
					176,
					48,
					103,
					100,
					38,
					56,
					176,
					26,
					34,
					3,
					32,
					57,
					0,
					255,
					130,
					186,
					144,
					65,
					49,
					192,
					176,
					52,
					8,
					248,
					0,
					7,
					176,
					146,
					34,
					60,
					0,
					74,
					252,
					0,
					104,
					179
				}, 0, 1038);
				Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
			}
		}

		private void WRITE1024WRITEBOOTLOADB(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[1038]
				{
					132,
					12,
					109,
					16,
					240,
					54,
					0,
					4,
					0,
					255,
					133,
					254,
					2,
					184,
					255,
					0,
					0,
					255,
					176,
					146,
					131,
					184,
					176,
					146,
					82,
					131,
					45,
					67,
					255,
					252,
					78,
					185,
					0,
					255,
					141,
					44,
					74,
					56,
					176,
					16,
					102,
					28,
					31,
					56,
					176,
					148,
					63,
					56,
					176,
					146,
					48,
					56,
					176,
					52,
					67,
					238,
					255,
					252,
					32,
					124,
					0,
					255,
					160,
					0,
					78,
					185,
					0,
					255,
					139,
					94,
					8,
					184,
					0,
					7,
					176,
					146,
					49,
					252,
					74,
					252,
					176,
					144,
					22,
					56,
					176,
					34,
					182,
					56,
					176,
					35,
					102,
					92,
					38,
					56,
					176,
					26,
					34,
					3,
					32,
					57,
					0,
					255,
					130,
					182,
					144,
					65,
					49,
					192,
					176,
					52,
					8,
					248,
					0,
					7,
					176,
					146,
					2,
					184,
					255,
					0,
					0,
					255,
					176,
					146,
					0,
					184,
					0,
					74,
					252,
					0,
					176,
					146,
					82,
					131,
					45,
					67,
					255,
					252,
					78,
					185,
					0,
					255,
					141,
					44,
					74,
					56,
					176,
					16,
					102,
					28,
					31,
					56,
					176,
					148,
					63,
					56,
					176,
					146,
					48,
					56,
					176,
					52,
					67,
					238,
					255,
					252,
					32,
					124,
					0,
					255,
					160,
					0,
					78,
					185,
					0,
					255,
					139,
					94,
					8,
					184,
					0,
					7,
					176,
					146,
					66,
					184,
					176,
					18,
					74,
					56,
					176,
					40,
					103,
					50,
					74,
					56,
					176,
					41,
					103,
					44,
					74,
					56,
					176,
					42,
					103,
					38,
					74,
					56,
					176,
					43,
					103,
					32,
					74,
					56,
					176,
					44,
					103,
					26,
					74,
					56,
					176,
					45,
					103,
					20,
					74,
					56,
					176,
					46,
					103,
					14,
					74,
					56,
					176,
					47,
					103,
					8,
					17,
					252,
					0,
					1,
					176,
					38,
					96,
					18,
					16,
					57,
					0,
					255,
					130,
					190,
					78,
					185,
					0,
					255,
					145,
					6,
					78,
					185,
					0,
					255,
					150,
					10,
					74,
					56,
					176,
					38,
					103,
					0,
					252,
					212,
					16,
					57,
					0,
					255,
					130,
					195,
					78,
					185,
					0,
					255,
					140,
					252,
					76,
					223,
					3,
					199,
					78,
					94,
					78,
					117,
					72,
					231,
					227,
					128,
					22,
					56,
					176,
					34,
					116,
					1,
					180,
					3,
					102,
					0,
					0,
					136,
					49,
					252,
					255,
					255,
					176,
					144,
					54,
					56,
					176,
					142,
					60,
					60,
					0,
					137,
					188,
					67,
					102,
					4,
					114,
					8,
					96,
					92,
					180,
					67,
					102,
					88,
					114,
					16,
					96,
					84,
					54,
					56,
					176,
					142,
					188,
					67,
					102,
					20,
					66,
					135,
					30,
					1,
					32,
					112,
					117,
					176,
					0,
					255,
					130,
					82,
					78,
					185,
					0,
					255,
					135,
					230,
					96,
					18,
					180,
					67,
					102,
					14,
					66,
					135,
					30,
					1,
					32,
					112,
					117,
					176,
					0,
					255,
					130,
					114,
					96,
					230,
					112,
					120,
					78,
					185,
					0,
					255,
					145,
					6,
					78,
					185,
					0,
					255,
					150,
					10,
					78,
					185,
					0,
					255,
					141,
					44,
					48,
					60,
					39,
					22,
					78,
					185,
					0,
					255,
					131,
					0,
					78,
					185,
					0,
					255,
					141,
					44,
					83,
					1,
					180,
					1,
					99,
					168,
					66,
					56,
					176,
					59,
					48,
					124,
					0,
					0,
					78,
					185,
					0,
					255,
					135,
					230,
					96,
					54,
					12,
					3,
					0,
					2,
					101,
					48,
					12,
					3,
					0,
					8,
					98,
					42,
					54,
					56,
					176,
					142,
					12,
					67,
					0,
					137,
					102,
					8,
					32,
					121,
					0,
					255,
					130,
					86,
					96,
					218,
					180,
					67,
					102,
					20,
					32,
					121,
					0,
					255,
					130,
					118,
					78,
					185,
					0,
					255,
					135,
					230,
					32,
					121,
					0,
					255,
					130,
					122,
					96,
					194,
					76,
					223,
					1,
					199,
					78,
					117,
					78,
					86,
					255,
					250,
					72,
					231,
					128,
					72,
					34,
					72,
					54,
					56,
					176,
					142,
					12,
					67,
					0,
					137,
					102,
					112,
					78,
					185,
					0,
					255,
					141,
					70,
					78,
					185,
					0,
					255,
					131,
					142,
					50,
					185,
					0,
					255,
					130,
					38,
					50,
					185,
					0,
					255,
					130,
					50,
					78,
					185,
					0,
					255,
					141,
					44,
					61,
					81,
					255,
					252,
					61,
					110,
					255,
					252,
					255,
					254,
					8,
					46,
					0,
					7,
					255,
					255,
					103,
					232,
					50,
					185,
					0,
					255,
					130,
					52,
					61,
					110,
					255,
					252,
					255,
					254,
					8,
					46,
					0,
					5,
					255,
					255,
					103,
					34,
					61,
					110,
					255,
					252,
					255,
					254,
					8,
					46,
					0,
					3,
					255,
					255,
					103,
					8,
					16,
					57,
					0,
					255,
					130,
					192,
					96,
					6,
					16,
					57,
					0,
					255,
					130,
					193,
					78,
					185,
					0,
					255,
					140,
					252,
					78,
					185,
					0,
					255,
					141,
					84,
					96,
					0,
					0,
					206,
					12,
					67,
					0,
					1,
					102,
					0,
					0,
					198,
					40,
					121,
					0,
					255,
					130,
					64,
					38,
					76,
					36,
					121,
					0,
					255,
					130,
					68,
					32,
					74,
					54,
					185,
					0,
					255,
					130,
					80,
					48,
					185,
					0,
					255,
					130,
					82,
					61,
					121,
					0,
					255,
					130,
					54,
					255,
					252,
					32,
					76,
					48,
					174,
					255,
					252,
					56,
					185,
					0,
					255,
					130,
					80,
					52,
					185,
					0,
					255,
					130,
					82,
					50,
					185,
					0,
					255,
					130,
					60,
					78,
					185,
					0,
					255,
					141,
					44,
					66,
					128,
					32,
					73,
					78,
					185,
					0,
					255,
					137,
					62,
					61,
					64,
					255,
					252,
					66,
					128,
					32,
					73,
					78,
					185,
					0,
					255,
					137,
					62,
					61,
					64,
					255,
					254,
					8,
					46,
					0,
					6,
					255,
					255,
					86,
					192,
					68,
					0,
					8,
					46,
					0,
					6,
					255,
					253,
					86,
					195,
					68,
					3,
					182,
					0,
					103,
					8,
					8,
					46,
					0,
					5,
					255,
					253,
					103,
					190,
					66,
					128,
					32,
					73,
					78,
					185,
					0,
					255,
					137,
					62,
					61,
					64,
					255,
					252,
					66,
					128,
					32,
					73,
					78,
					185,
					0,
					255,
					137,
					62,
					61,
					64,
					255,
					254,
					8,
					46,
					0,
					6,
					255,
					255,
					86,
					192,
					68,
					0,
					8,
					46,
					0,
					6,
					255,
					253,
					86,
					195,
					68,
					3,
					182,
					0,
					103,
					12,
					16,
					57,
					0,
					255,
					130,
					193,
					78,
					185,
					0,
					255,
					140,
					252,
					50,
					185,
					0,
					255,
					130,
					62,
					76,
					223,
					18,
					1,
					78,
					94,
					78,
					117,
					78,
					86,
					255,
					252,
					208,
					128,
					208,
					136,
					45,
					64,
					255,
					252,
					36,
					110,
					255,
					252,
					48,
					18,
					78,
					94,
					78,
					117,
					66,
					184,
					176,
					18,
					8,
					184,
					0,
					7,
					176,
					146,
					118,
					1,
					66,
					132,
					24,
					3,
					66,
					48,
					65,
					160,
					176,
					39,
					82,
					3,
					12,
					3,
					0,
					8,
					99,
					238,
					66,
					56,
					176,
					49,
					66,
					56,
					176,
					58,
					66,
					56,
					176,
					38,
					66,
					56,
					176,
					50,
					78,
					117,
					72,
					231,
					224,
					128,
					32,
					124,
					0,
					255,
					160,
					0,
					49,
					208,
					176,
					36,
					84,
					136,
					54,
					16,
					40,
					3,
					17,
					196,
					176,
					34,
					12,
					67,
					0,
					2,
					101,
					80,
					12,
					67,
					0,
					8,
					98,
					74,
					216,
					4,
					2,
					68,
					0,
					255,
					33,
					240,
					69,
					160,
					176,
					64,
					176,
					22,
					33,
					240,
					69,
					160,
					176,
					68,
					176,
					26,
					74,
					56,
					176,
					49,
					102,
					126,
					66,
					128,
					120,
					2,
					34,
					4,
					36,
					1,
					212,
					2,
					2,
					66,
					0,
					255,
					38,
					48,
					37,
					160,
					176,
					68,
					182,
					128,
					99,
					6,
					32,
					3,
					17,
					193,
					176,
					35,
					82,
					68,
					12,
					68,
					0,
					8,
					99,
					222,
					17,
					252,
					0,
					1,
					176,
					49,
					96,
					80,
					34,
					60,
					0,
					255,
					160,
					0,
					6,
					129,
					0,
					0,
					5,
					0,
					162,
					120
				}, 0, 1038);
				Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
			}
		}

		private void WRITE1024WRITEBOOTLOADC(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[1038]
				{
					132,
					12,
					109,
					16,
					240,
					54,
					0,
					4,
					0,
					255,
					137,
					254,
					33,
					193,
					176,
					136,
					32,
					65,
					69,
					248,
					176,
					60,
					118,
					37,
					52,
					216,
					81,
					203,
					255,
					252,
					49,
					248,
					176,
					60,
					176,
					36,
					54,
					56,
					176,
					62,
					17,
					195,
					176,
					34,
					114,
					1,
					17,
					193,
					176,
					48,
					178,
					3,
					103,
					14,
					16,
					57,
					0,
					255,
					130,
					199,
					78,
					185,
					0,
					255,
					140,
					252,
					96,
					12,
					33,
					248,
					176,
					72,
					176,
					22,
					33,
					248,
					176,
					76,
					176,
					26,
					38,
					56,
					176,
					26,
					150,
					184,
					176,
					22,
					82,
					131,
					33,
					195,
					176,
					30,
					66,
					67,
					22,
					56,
					176,
					34,
					74,
					48,
					49,
					160,
					176,
					39,
					103,
					14,
					16,
					57,
					0,
					255,
					130,
					199,
					78,
					185,
					0,
					255,
					140,
					252,
					96,
					8,
					31,
					188,
					0,
					1,
					49,
					160,
					176,
					39,
					76,
					223,
					1,
					7,
					78,
					117,
					78,
					86,
					255,
					254,
					72,
					231,
					224,
					192,
					114,
					1,
					17,
					193,
					176,
					59,
					66,
					56,
					176,
					16,
					50,
					124,
					5,
					0,
					33,
					201,
					176,
					136,
					69,
					248,
					176,
					60,
					118,
					37,
					52,
					217,
					81,
					203,
					255,
					252,
					36,
					124,
					0,
					15,
					255,
					254,
					49,
					210,
					176,
					144,
					49,
					249,
					0,
					255,
					130,
					206,
					250,
					76,
					49,
					249,
					0,
					255,
					130,
					200,
					250,
					74,
					49,
					249,
					0,
					255,
					130,
					202,
					250,
					78,
					52,
					124,
					0,
					0,
					52,
					185,
					0,
					255,
					130,
					46,
					54,
					18,
					49,
					195,
					176,
					142,
					52,
					60,
					0,
					137,
					180,
					67,
					103,
					46,
					36,
					121,
					0,
					255,
					130,
					64,
					38,
					74,
					32,
					121,
					0,
					255,
					130,
					68,
					54,
					185,
					0,
					255,
					130,
					80,
					48,
					185,
					0,
					255,
					130,
					82,
					61,
					121,
					0,
					255,
					130,
					46,
					255,
					254,
					52,
					174,
					255,
					254,
					52,
					124,
					0,
					0,
					49,
					210,
					176,
					142,
					52,
					124,
					0,
					2,
					56,
					18,
					49,
					196,
					176,
					140,
					52,
					124,
					0,
					0,
					54,
					56,
					176,
					142,
					180,
					67,
					102,
					8,
					52,
					185,
					0,
					255,
					130,
					52,
					96,
					10,
					178,
					67,
					102,
					6,
					52,
					185,
					0,
					255,
					130,
					62,
					180,
					67,
					102,
					6,
					12,
					68,
					136,
					157,
					103,
					22,
					178,
					67,
					102,
					6,
					12,
					68,
					34,
					88,
					103,
					12,
					16,
					57,
					0,
					255,
					130,
					198,
					78,
					185,
					0,
					255,
					140,
					252,
					78,
					185,
					0,
					255,
					131,
					192,
					76,
					223,
					3,
					7,
					78,
					94,
					78,
					117,
					78,
					86,
					255,
					242,
					72,
					231,
					98,
					8,
					54,
					56,
					176,
					142,
					12,
					67,
					0,
					137,
					102,
					0,
					0,
					162,
					29,
					110,
					0,
					10,
					255,
					244,
					61,
					110,
					0,
					8,
					255,
					242,
					78,
					185,
					0,
					255,
					141,
					70,
					78,
					185,
					0,
					255,
					131,
					142,
					52,
					0,
					114,
					1,
					12,
					66,
					0,
					1,
					96,
					114,
					36,
					81,
					52,
					185,
					0,
					255,
					130,
					40,
					8,
					46,
					0,
					7,
					255,
					242,
					102,
					4,
					54,
					16,
					96,
					6,
					38,
					46,
					255,
					242,
					224,
					139,
					36,
					81,
					52,
					131,
					36,
					81,
					52,
					185,
					0,
					255,
					130,
					44,
					78,
					185,
					0,
					255,
					141,
					44,
					36,
					81,
					61,
					82,
					255,
					246,
					61,
					110,
					255,
					246,
					255,
					248,
					8,
					46,
					0,
					7,
					255,
					249,
					103,
					230,
					61,
					110,
					255,
					246,
					255,
					248,
					8,
					46,
					0,
					4,
					255,
					249,
					103,
					12,
					16,
					57,
					0,
					255,
					130,
					194,
					78,
					185,
					0,
					255,
					140,
					252,
					36,
					81,
					52,
					185,
					0,
					255,
					130,
					52,
					84,
					136,
					36,
					73,
					38,
					18,
					84,
					131,
					36,
					131,
					84,
					65,
					101,
					4,
					180,
					65,
					100,
					140,
					78,
					185,
					0,
					255,
					141,
					84,
					96,
					0,
					0,
					200,
					124,
					1,
					188,
					67,
					102,
					0,
					0,
					192,
					29,
					110,
					0,
					10,
					255,
					244,
					61,
					110,
					0,
					8,
					255,
					242,
					52,
					0,
					34,
					6,
					188,
					66,
					98,
					0,
					0,
					170,
					8,
					46,
					0,
					7,
					255,
					242,
					102,
					4,
					48,
					16,
					96,
					6,
					32,
					46,
					255,
					242,
					224,
					136,
					36,
					121,
					0,
					255,
					130,
					64,
					38,
					74,
					40,
					121,
					0,
					255,
					130,
					68,
					54,
					185,
					0,
					255,
					130,
					80,
					56,
					185,
					0,
					255,
					130,
					82,
					61,
					121,
					0,
					255,
					130,
					56,
					255,
					254,
					52,
					174,
					255,
					254,
					52,
					64,
					38,
					81,
					54,
					138,
					61,
					74,
					255,
					248,
					78,
					185,
					0,
					255,
					141,
					44,
					36,
					81,
					61,
					82,
					255,
					246,
					8,
					46,
					0,
					7,
					255,
					247,
					86,
					195,
					68,
					3,
					8,
					46,
					0,
					7,
					255,
					249,
					86,
					196,
					68,
					4,
					184,
					3,
					103,
					8,
					8,
					46,
					0,
					5,
					255,
					247,
					103,
					212,
					36,
					81,
					52,
					82,
					61,
					74,
					255,
					246,
					61,
					74,
					255,
					250,
					176,
					110,
					255,
					250,
					103,
					12,
					16,
					57,
					0,
					255,
					130,
					194,
					78,
					185,
					0,
					255,
					140,
					252,
					36,
					81,
					52,
					185,
					0,
					255,
					130,
					62,
					84,
					136,
					36,
					73,
					38,
					18,
					84,
					131,
					36,
					131,
					84,
					65,
					101,
					6,
					180,
					65,
					100,
					0,
					255,
					90,
					76,
					223,
					16,
					70,
					78,
					94,
					78,
					116,
					0,
					4,
					17,
					252,
					0,
					1,
					176,
					16,
					74,
					56,
					176,
					59,
					103,
					6,
					78,
					185,
					0,
					255,
					130,
					250,
					78,
					185,
					0,
					255,
					137,
					84,
					78,
					117,
					78,
					185,
					0,
					255,
					145,
					6,
					78,
					185,
					0,
					255,
					150,
					10,
					78,
					185,
					0,
					255,
					141,
					44,
					48,
					60,
					39,
					22,
					78,
					185,
					0,
					255,
					131,
					0,
					78,
					185,
					0,
					255,
					141,
					44,
					78,
					185,
					0,
					255,
					150,
					46,
					78,
					185,
					0,
					255,
					140,
					226,
					78,
					117,
					17,
					252,
					0,
					85,
					250,
					39,
					17,
					252,
					0,
					170,
					250,
					39,
					8,
					184,
					0,
					7,
					208,
					6,
					8,
					248,
					0,
					7,
					208,
					6,
					78,
					117,
					54,
					56,
					226,
					250,
					8,
					195,
					0,
					0,
					49,
					195,
					226,
					250,
					78,
					117,
					47,
					1,
					54,
					56,
					226,
					250,
					8,
					131,
					0,
					0,
					49,
					195,
					226,
					250,
					114,
					1,
					78,
					185,
					0,
					255,
					141,
					44,
					48,
					60,
					39,
					22,
					78,
					185,
					0,
					255,
					131,
					0,
					78,
					185,
					0,
					255,
					141,
					44,
					82,
					65,
					12,
					65,
					0,
					15,
					99,
					226,
					78,
					185,
					0,
					255,
					141,
					150,
					74,
					0,
					103,
					4,
					112,
					1,
					96,
					2,
					66,
					0,
					34,
					31,
					78,
					117,
					54,
					56,
					225,
					18,
					8,
					3,
					0,
					8,
					86,
					192,
					68,
					0,
					78,
					117,
					49,
					249,
					0,
					255,
					130,
					224,
					242,
					8,
					49,
					249,
					0,
					255,
					130,
					222,
					242,
					0,
					49,
					249,
					0,
					255,
					130,
					226,
					242,
					10,
					49,
					249,
					0,
					255,
					130,
					228,
					242,
					12,
					49,
					249,
					0,
					255,
					130,
					230,
					242,
					14,
					49,
					252,
					0,
					6,
					242,
					48,
					66,
					120,
					242,
					16,
					78,
					117,
					8,
					248,
					0,
					5,
					242,
					12,
					112,
					93,
					78,
					185,
					0,
					255,
					131,
					0,
					8,
					56,
					0,
					7,
					242,
					16,
					102,
					4,
					66,
					64,
					96,
					10,
					8,
					184,
					0,
					7,
					242,
					16,
					48,
					56,
					242,
					176,
					78,
					117,
					172,
					47
				}, 0, 1038);
				Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
			}
		}

		private void WRITE1024WRITEBOOTLOADD(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[1038]
				{
					132,
					12,
					109,
					16,
					240,
					54,
					0,
					4,
					0,
					255,
					145,
					6,
					17,
					252,
					0,
					108,
					175,
					194,
					17,
					252,
					0,
					240,
					175,
					195,
					17,
					252,
					0,
					16,
					175,
					196,
					17,
					252,
					0,
					118,
					175,
					197,
					66,
					56,
					175,
					198,
					17,
					192,
					175,
					199,
					17,
					252,
					0,
					6,
					175,
					206,
					78,
					117,
					78,
					185,
					0,
					255,
					141,
					44,
					78,
					117,
					49,
					249,
					0,
					255,
					130,
					210,
					246,
					0,
					78,
					117,
					47,
					0,
					49,
					249,
					0,
					255,
					130,
					216,
					246,
					12,
					112,
					1,
					78,
					185,
					0,
					255,
					145,
					46,
					82,
					0,
					12,
					0,
					0,
					5,
					99,
					242,
					32,
					31,
					78,
					117,
					66,
					120,
					175,
					174,
					66,
					56,
					175,
					172,
					78,
					117,
					78,
					86,
					255,
					254,
					72,
					231,
					226,
					0,
					22,
					56,
					175,
					160,
					29,
					121,
					0,
					255,
					130,
					247,
					255,
					254,
					29,
					67,
					255,
					255,
					49,
					238,
					255,
					254,
					246,
					12,
					20,
					56,
					175,
					172,
					83,
					2,
					112,
					2,
					12,
					2,
					0,
					2,
					96,
					26,
					66,
					134,
					28,
					0,
					18,
					48,
					97,
					160,
					175,
					159,
					78,
					185,
					0,
					255,
					145,
					46,
					17,
					193,
					246,
					13,
					82,
					0,
					101,
					4,
					180,
					0,
					100,
					228,
					66,
					67,
					22,
					56,
					175,
					172,
					22,
					48,
					49,
					160,
					175,
					159,
					29,
					121,
					0,
					255,
					130,
					246,
					255,
					254,
					29,
					67,
					255,
					255,
					49,
					238,
					255,
					254,
					246,
					12,
					76,
					223,
					0,
					71,
					78,
					94,
					78,
					117,
					78,
					86,
					255,
					254,
					29,
					120,
					246,
					14,
					255,
					254,
					8,
					46,
					0,
					3,
					255,
					254,
					103,
					8,
					49,
					249,
					0,
					255,
					130,
					218,
					246,
					12,
					22,
					46,
					255,
					254,
					2,
					3,
					0,
					3,
					102,
					224,
					78,
					94,
					78,
					117,
					72,
					231,
					192,
					0,
					17,
					252,
					0,
					108,
					175,
					194,
					17,
					252,
					0,
					240,
					175,
					195,
					17,
					252,
					0,
					16,
					175,
					196,
					17,
					252,
					0,
					118,
					175,
					197,
					66,
					56,
					175,
					198,
					16,
					56,
					175,
					192,
					85,
					0,
					118,
					5,
					12,
					0,
					0,
					5,
					96,
					22,
					66,
					129,
					18,
					3,
					66,
					68,
					24,
					48,
					17,
					160,
					175,
					179,
					217,
					120,
					175,
					176,
					82,
					3,
					101,
					4,
					176,
					3,
					100,
					232,
					66,
					69,
					26,
					56,
					175,
					191,
					66,
					67,
					22,
					56,
					175,
					190,
					225,
					67,
					214,
					69,
					182,
					120,
					175,
					176,
					103,
					24,
					17,
					252,
					0,
					119,
					175,
					199,
					17,
					252,
					0,
					6,
					175,
					206,
					66,
					56,
					175,
					178,
					78,
					185,
					0,
					255,
					150,
					10,
					96,
					30,
					74,
					56,
					175,
					193,
					103,
					18,
					17,
					252,
					0,
					114,
					175,
					199,
					66,
					56,
					175,
					178,
					112,
					6,
					17,
					192,
					175,
					206,
					96,
					224,
					17,
					252,
					0,
					1,
					175,
					178,
					17,
					252,
					0,
					6,
					175,
					206,
					76,
					223,
					0,
					3,
					78,
					117,
					78,
					86,
					255,
					250,
					72,
					231,
					224,
					192,
					12,
					56,
					0,
					16,
					175,
					181,
					102,
					0,
					1,
					72,
					66,
					120,
					175,
					176,
					66,
					69,
					26,
					56,
					175,
					186,
					66,
					67,
					22,
					56,
					175,
					185,
					225,
					67,
					214,
					69,
					49,
					195,
					175,
					174,
					74,
					67,
					99,
					76,
					66,
					131,
					22,
					56,
					175,
					188,
					225,
					67,
					2,
					131,
					0,
					0,
					255,
					255,
					66,
					130,
					20,
					56,
					175,
					187,
					225,
					130,
					225,
					130,
					212,
					131,
					66,
					131,
					22,
					56,
					175,
					189,
					212,
					131,
					45,
					66,
					255,
					250,
					78,
					185,
					0,
					255,
					148,
					28,
					36,
					110,
					255,
					250,
					20,
					128,
					74,
					56,
					175,
					207,
					102,
					20,
					82,
					138,
					45,
					74,
					255,
					250,
					54,
					56,
					175,
					174,
					83,
					67,
					49,
					195,
					175,
					174,
					74,
					67,
					102,
					218,
					74,
					56,
					175,
					207,
					102,
					0,
					0,
					226,
					78,
					185,
					0,
					255,
					150,
					56,
					74,
					56,
					175,
					207,
					102,
					20,
					17,
					192,
					175,
					190,
					78,
					185,
					0,
					255,
					150,
					56,
					17,
					192,
					175,
					191,
					17,
					252,
					0,
					12,
					175,
					192,
					74,
					56,
					175,
					207,
					102,
					0,
					0,
					186,
					66,
					0,
					82,
					129,
					12,
					129,
					0,
					3,
					13,
					64,
					99,
					16,
					112,
					1,
					78,
					185,
					0,
					255,
					140,
					226,
					17,
					192,
					175,
					207,
					96,
					0,
					0,
					134,
					78,
					185,
					0,
					255,
					141,
					44,
					29,
					120,
					246,
					14,
					255,
					254,
					22,
					46,
					255,
					254,
					234,
					11,
					103,
					106,
					112,
					1,
					22,
					46,
					255,
					254,
					234,
					11,
					12,
					3,
					0,
					5,
					103,
					24,
					22,
					46,
					255,
					254,
					234,
					11,
					12,
					3,
					0,
					6,
					103,
					12,
					22,
					46,
					255,
					254,
					234,
					11,
					12,
					3,
					0,
					7,
					102,
					56,
					67,
					238,
					255,
					255,
					65,
					238,
					255,
					254,
					78,
					185,
					0,
					255,
					148,
					6,
					66,
					3,
					8,
					46,
					0,
					7,
					255,
					255,
					102,
					24,
					8,
					46,
					0,
					6,
					255,
					255,
					102,
					16,
					8,
					46,
					0,
					3,
					255,
					255,
					102,
					8,
					8,
					46,
					0,
					2,
					255,
					255,
					103,
					2,
					82,
					3,
					17,
					195,
					175,
					193,
					96,
					12,
					78,
					185,
					0,
					255,
					145,
					64,
					17,
					252,
					0,
					1,
					175,
					193,
					74,
					0,
					103,
					0,
					255,
					100,
					74,
					56,
					175,
					207,
					102,
					18,
					78,
					185,
					0,
					255,
					145,
					254,
					96,
					10,
					78,
					185,
					0,
					255,
					145,
					64,
					66,
					56,
					175,
					178,
					76,
					223,
					3,
					7,
					78,
					94,
					78,
					117,
					78,
					86,
					255,
					254,
					61,
					120,
					246,
					14,
					255,
					254,
					16,
					174,
					255,
					254,
					18,
					174,
					255,
					255,
					78,
					94,
					78,
					117,
					78,
					185,
					0,
					255,
					150,
					56,
					66,
					131,
					22,
					0,
					215,
					120,
					175,
					176,
					78,
					117,
					78,
					86,
					255,
					254,
					72,
					231,
					227,
					192,
					66,
					128,
					66,
					56,
					175,
					178,
					36,
					0,
					34,
					0,
					66,
					56,
					175,
					207,
					82,
					129,
					12,
					129,
					0,
					6,
					239,
					145,
					99,
					16,
					116,
					1,
					78,
					185,
					0,
					255,
					140,
					226,
					17,
					194,
					175,
					207,
					96,
					0,
					1,
					168,
					29,
					120,
					246,
					14,
					255,
					254,
					22,
					46,
					255,
					254,
					234,
					11,
					103,
					14,
					67,
					238,
					255,
					255,
					65,
					238,
					255,
					254,
					78,
					185,
					0,
					255,
					148,
					6,
					17,
					252,
					0,
					108,
					175,
					194,
					17,
					252,
					0,
					240,
					175,
					195,
					124,
					16,
					17,
					198,
					175,
					196,
					22,
					46,
					255,
					254,
					234,
					11,
					103,
					0,
					1,
					92,
					12,
					3,
					0,
					4,
					98,
					66,
					74,
					3,
					103,
					0,
					1,
					80,
					82,
					0,
					66,
					135,
					30,
					0,
					31,
					174,
					255,
					255,
					113,
					160,
					175,
					179,
					12,
					0,
					0,
					10,
					102,
					0,
					1,
					58,
					12,
					56,
					0,
					54,
					175,
					183,
					102,
					16,
					78,
					185,
					0,
					255,
					146,
					156,
					74,
					56,
					175,
					207,
					103,
					10,
					96,
					0,
					1,
					54,
					78,
					185,
					0,
					255,
					145,
					64,
					66,
					0,
					116,
					1,
					96,
					0,
					1,
					20,
					66,
					56,
					175,
					172,
					22,
					56,
					175,
					183,
					12,
					3,
					0,
					160,
					102,
					44,
					22,
					56,
					175,
					181,
					188,
					3,
					103,
					8,
					12,
					3,
					0,
					254,
					102,
					0,
					0,
					244,
					17,
					252,
					0,
					224,
					175,
					197,
					17,
					252,
					0,
					170,
					175,
					198,
					188,
					1
				}, 0, 1038);
				Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
			}
		}

		private void WRITE1024WRITEBOOTLOADE(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[458]
				{
					129,
					200,
					109,
					16,
					240,
					54,
					0,
					1,
					188,
					255,
					149,
					6,
					17,
					252,
					0,
					5,
					175,
					206,
					78,
					185,
					0,
					255,
					150,
					10,
					96,
					0,
					0,
					216,
					12,
					3,
					0,
					161,
					102,
					14,
					78,
					185,
					0,
					255,
					145,
					54,
					66,
					56,
					175,
					183,
					96,
					0,
					0,
					196,
					12,
					3,
					0,
					40,
					102,
					12,
					17,
					252,
					0,
					104,
					175,
					197,
					66,
					56,
					175,
					198,
					96,
					202,
					12,
					3,
					0,
					39,
					102,
					28,
					17,
					252,
					0,
					103,
					175,
					197,
					17,
					252,
					0,
					1,
					175,
					198,
					66,
					56,
					175,
					199,
					66,
					56,
					175,
					200,
					17,
					252,
					0,
					7,
					175,
					206,
					96,
					174,
					12,
					3,
					0,
					52,
					102,
					36,
					17,
					252,
					0,
					1,
					176,
					16,
					17,
					252,
					0,
					116,
					175,
					197,
					66,
					56,
					175,
					198,
					17,
					252,
					0,
					74,
					175,
					199,
					17,
					252,
					0,
					6,
					175,
					206,
					78,
					185,
					0,
					255,
					150,
					10,
					96,
					122,
					12,
					3,
					0,
					60,
					102,
					82,
					78,
					185,
					0,
					255,
					131,
					74,
					17,
					252,
					0,
					124,
					175,
					197,
					17,
					252,
					0,
					20,
					175,
					198,
					52,
					124,
					64,
					136,
					54,
					18,
					12,
					67,
					165,
					160,
					102,
					6,
					52,
					124,
					64,
					28,
					96,
					4,
					52,
					124,
					96,
					28,
					17,
					210,
					175,
					199,
					82,
					138,
					17,
					210,
					175,
					200,
					82,
					138,
					17,
					210,
					175,
					201,
					82,
					138,
					17,
					210,
					175,
					202,
					17,
					252,
					0,
					9,
					175,
					206,
					78,
					185,
					0,
					255,
					150,
					10,
					78,
					185,
					0,
					255,
					131,
					48,
					96,
					12,
					12,
					3,
					0,
					63,
					102,
					6,
					66,
					129,
					66,
					56,
					175,
					207,
					66,
					0,
					78,
					185,
					0,
					255,
					141,
					44,
					74,
					2,
					103,
					0,
					254,
					74,
					74,
					56,
					175,
					178,
					103,
					0,
					254,
					66,
					76,
					223,
					3,
					199,
					78,
					94,
					78,
					117,
					78,
					185,
					0,
					255,
					145,
					214,
					71,
					248,
					175,
					194,
					69,
					248,
					175,
					160,
					118,
					5,
					52,
					219,
					81,
					203,
					255,
					252,
					20,
					219,
					78,
					185,
					0,
					255,
					145,
					104,
					66,
					56,
					175,
					172,
					78,
					117,
					49,
					249,
					0,
					255,
					130,
					214,
					246,
					12,
					78,
					117,
					78,
					86,
					255,
					252,
					72,
					231,
					96,
					0,
					66,
					130,
					34,
					2,
					82,
					129,
					12,
					129,
					0,
					4,
					124,
					229,
					99,
					14,
					116,
					1,
					78,
					185,
					0,
					255,
					140,
					226,
					17,
					194,
					175,
					207,
					96,
					88,
					29,
					120,
					246,
					14,
					255,
					252,
					78,
					185,
					0,
					255,
					141,
					44,
					78,
					185,
					0,
					255,
					141,
					44,
					78,
					185,
					0,
					255,
					141,
					44,
					22,
					46,
					255,
					252,
					234,
					11,
					103,
					48,
					61,
					120,
					246,
					14,
					255,
					254,
					29,
					110,
					255,
					254,
					255,
					252,
					16,
					46,
					255,
					255,
					116,
					1,
					22,
					46,
					255,
					252,
					234,
					11,
					12,
					3,
					0,
					7,
					103,
					12,
					12,
					3,
					0,
					5,
					103,
					6,
					12,
					3,
					0,
					6,
					102,
					6,
					49,
					252,
					0,
					1,
					175,
					174,
					74,
					2,
					103,
					148,
					74,
					2,
					102,
					4,
					16,
					60,
					0,
					255,
					76,
					223,
					0,
					6,
					78,
					94,
					78,
					117,
					78,
					113,
					184,
					174
				}, 0, 458);
				Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
			}
		}

		private void WRITE512WRITEBOOTLOADA(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[2062]
				{
					136,
					12,
					109,
					16,
					240,
					54,
					0,
					8,
					0,
					255,
					129,
					238,
					0,
					124,
					7,
					0,
					157,
					206,
					46,
					124,
					0,
					255,
					184,
					0,
					74,
					121,
					0,
					255,
					130,
					94,
					103,
					6,
					78,
					185,
					0,
					255,
					145,
					44,
					36,
					121,
					0,
					255,
					130,
					90,
					78,
					146,
					78,
					114,
					39,
					0,
					32,
					32,
					64,
					64,
					112,
					112,
					144,
					144,
					208,
					208,
					255,
					255,
					0,
					1,
					255,
					255,
					0,
					7,
					255,
					255,
					115,
					131,
					132,
					133,
					133,
					134,
					135,
					136,
					137,
					138,
					104,
					32,
					112,
					96,
					16,
					96,
					0,
					6,
					0,
					133,
					8,
					133,
					112,
					0,
					64,
					0,
					3,
					0,
					224,
					0,
					3,
					255,
					0,
					0,
					0,
					0,
					128,
					106,
					1,
					0,
					0,
					1,
					0,
					255,
					184,
					0,
					0,
					255,
					129,
					238,
					0,
					255,
					136,
					40,
					0,
					0,
					12,
					20,
					28,
					255,
					32,
					23,
					78,
					114,
					39,
					0,
					12,
					64,
					0,
					2,
					99,
					12,
					83,
					64,
					78,
					113,
					78,
					113,
					78,
					113,
					83,
					64,
					102,
					246,
					78,
					117,
					17,
					252,
					0,
					108,
					175,
					194,
					17,
					252,
					0,
					240,
					175,
					195,
					17,
					252,
					0,
					16,
					175,
					196,
					17,
					252,
					0,
					118,
					175,
					197,
					66,
					56,
					175,
					198,
					17,
					192,
					175,
					199,
					17,
					252,
					0,
					6,
					175,
					206,
					78,
					117,
					78,
					185,
					0,
					255,
					144,
					96,
					78,
					117,
					49,
					249,
					0,
					255,
					130,
					60,
					246,
					0,
					78,
					117,
					47,
					0,
					49,
					249,
					0,
					255,
					130,
					66,
					246,
					12,
					112,
					1,
					78,
					185,
					0,
					255,
					130,
					166,
					82,
					0,
					12,
					0,
					0,
					5,
					99,
					242,
					32,
					31,
					78,
					117,
					66,
					120,
					175,
					174,
					66,
					56,
					175,
					172,
					78,
					117,
					78,
					86,
					255,
					254,
					72,
					231,
					226,
					0,
					22,
					56,
					175,
					160,
					29,
					121,
					0,
					255,
					130,
					97,
					255,
					254,
					29,
					67,
					255,
					255,
					49,
					238,
					255,
					254,
					246,
					12,
					20,
					56,
					175,
					172,
					83,
					2,
					112,
					2,
					12,
					2,
					0,
					2,
					96,
					26,
					66,
					134,
					28,
					0,
					18,
					48,
					97,
					160,
					175,
					159,
					78,
					185,
					0,
					255,
					130,
					166,
					17,
					193,
					246,
					13,
					82,
					0,
					101,
					4,
					180,
					0,
					100,
					228,
					66,
					67,
					22,
					56,
					175,
					172,
					22,
					48,
					49,
					160,
					175,
					159,
					29,
					121,
					0,
					255,
					130,
					96,
					255,
					254,
					29,
					67,
					255,
					255,
					49,
					238,
					255,
					254,
					246,
					12,
					76,
					223,
					0,
					71,
					78,
					94,
					78,
					117,
					78,
					86,
					255,
					254,
					29,
					120,
					246,
					14,
					255,
					254,
					8,
					46,
					0,
					3,
					255,
					254,
					103,
					8,
					49,
					249,
					0,
					255,
					130,
					68,
					246,
					12,
					22,
					46,
					255,
					254,
					2,
					3,
					0,
					3,
					102,
					224,
					78,
					94,
					78,
					117,
					72,
					231,
					192,
					0,
					17,
					252,
					0,
					108,
					175,
					194,
					17,
					252,
					0,
					240,
					175,
					195,
					17,
					252,
					0,
					16,
					175,
					196,
					17,
					252,
					0,
					118,
					175,
					197,
					66,
					56,
					175,
					198,
					16,
					56,
					175,
					192,
					85,
					0,
					118,
					5,
					12,
					0,
					0,
					5,
					96,
					22,
					66,
					129,
					18,
					3,
					66,
					68,
					24,
					48,
					17,
					160,
					175,
					179,
					217,
					120,
					175,
					176,
					82,
					3,
					101,
					4,
					176,
					3,
					100,
					232,
					66,
					69,
					26,
					56,
					175,
					191,
					66,
					67,
					22,
					56,
					175,
					190,
					225,
					67,
					214,
					69,
					182,
					120,
					175,
					176,
					103,
					24,
					17,
					252,
					0,
					119,
					175,
					199,
					17,
					252,
					0,
					6,
					175,
					206,
					66,
					56,
					175,
					178,
					78,
					185,
					0,
					255,
					135,
					114,
					96,
					30,
					74,
					56,
					175,
					193,
					103,
					18,
					17,
					252,
					0,
					114,
					175,
					199,
					66,
					56,
					175,
					178,
					112,
					6,
					17,
					192,
					175,
					206,
					96,
					224,
					17,
					252,
					0,
					1,
					175,
					178,
					17,
					252,
					0,
					6,
					175,
					206,
					76,
					223,
					0,
					3,
					78,
					117,
					78,
					86,
					255,
					250,
					72,
					231,
					224,
					192,
					12,
					56,
					0,
					16,
					175,
					181,
					102,
					0,
					1,
					72,
					66,
					120,
					175,
					176,
					66,
					69,
					26,
					56,
					175,
					186,
					66,
					67,
					22,
					56,
					175,
					185,
					225,
					67,
					214,
					69,
					49,
					195,
					175,
					174,
					74,
					67,
					99,
					76,
					66,
					131,
					22,
					56,
					175,
					188,
					225,
					67,
					2,
					131,
					0,
					0,
					255,
					255,
					66,
					130,
					20,
					56,
					175,
					187,
					225,
					130,
					225,
					130,
					212,
					131,
					66,
					131,
					22,
					56,
					175,
					189,
					212,
					131,
					45,
					66,
					255,
					250,
					78,
					185,
					0,
					255,
					133,
					148,
					36,
					110,
					255,
					250,
					20,
					128,
					74,
					56,
					175,
					207,
					102,
					20,
					82,
					138,
					45,
					74,
					255,
					250,
					54,
					56,
					175,
					174,
					83,
					67,
					49,
					195,
					175,
					174,
					74,
					67,
					102,
					218,
					74,
					56,
					175,
					207,
					102,
					0,
					0,
					226,
					78,
					185,
					0,
					255,
					135,
					160,
					74,
					56,
					175,
					207,
					102,
					20,
					17,
					192,
					175,
					190,
					78,
					185,
					0,
					255,
					135,
					160,
					17,
					192,
					175,
					191,
					17,
					252,
					0,
					12,
					175,
					192,
					74,
					56,
					175,
					207,
					102,
					0,
					0,
					186,
					66,
					0,
					82,
					129,
					12,
					129,
					0,
					3,
					13,
					64,
					99,
					16,
					112,
					1,
					78,
					185,
					0,
					255,
					143,
					244,
					17,
					192,
					175,
					207,
					96,
					0,
					0,
					134,
					78,
					185,
					0,
					255,
					144,
					96,
					29,
					120,
					246,
					14,
					255,
					254,
					22,
					46,
					255,
					254,
					234,
					11,
					103,
					106,
					112,
					1,
					22,
					46,
					255,
					254,
					234,
					11,
					12,
					3,
					0,
					5,
					103,
					24,
					22,
					46,
					255,
					254,
					234,
					11,
					12,
					3,
					0,
					6,
					103,
					12,
					22,
					46,
					255,
					254,
					234,
					11,
					12,
					3,
					0,
					7,
					102,
					56,
					67,
					238,
					255,
					255,
					65,
					238,
					255,
					254,
					78,
					185,
					0,
					255,
					133,
					126,
					66,
					3,
					8,
					46,
					0,
					7,
					255,
					255,
					102,
					24,
					8,
					46,
					0,
					6,
					255,
					255,
					102,
					16,
					8,
					46,
					0,
					3,
					255,
					255,
					102,
					8,
					8,
					46,
					0,
					2,
					255,
					255,
					103,
					2,
					82,
					3,
					17,
					195,
					175,
					193,
					96,
					12,
					78,
					185,
					0,
					255,
					130,
					184,
					17,
					252,
					0,
					1,
					175,
					193,
					74,
					0,
					103,
					0,
					255,
					100,
					74,
					56,
					175,
					207,
					102,
					18,
					78,
					185,
					0,
					255,
					131,
					118,
					96,
					10,
					78,
					185,
					0,
					255,
					130,
					184,
					66,
					56,
					175,
					178,
					76,
					223,
					3,
					7,
					78,
					94,
					78,
					117,
					78,
					86,
					255,
					254,
					61,
					120,
					246,
					14,
					255,
					254,
					16,
					174,
					255,
					254,
					18,
					174,
					255,
					255,
					78,
					94,
					78,
					117,
					78,
					185,
					0,
					255,
					135,
					160,
					66,
					131,
					22,
					0,
					215,
					120,
					175,
					176,
					78,
					117,
					78,
					86,
					255,
					254,
					72,
					231,
					227,
					192,
					66,
					128,
					66,
					56,
					175,
					178,
					36,
					0,
					34,
					0,
					66,
					56,
					175,
					207,
					82,
					129,
					12,
					129,
					0,
					5,
					222,
					103,
					99,
					16,
					116,
					1,
					78,
					185,
					0,
					255,
					143,
					244,
					17,
					194,
					175,
					207,
					96,
					0,
					1,
					152,
					29,
					120,
					246,
					14,
					255,
					254,
					22,
					46,
					255,
					254,
					234,
					11,
					103,
					14,
					67,
					238,
					255,
					255,
					65,
					238,
					255,
					254,
					78,
					185,
					0,
					255,
					133,
					126,
					17,
					252,
					0,
					108,
					175,
					194,
					17,
					252,
					0,
					240,
					175,
					195,
					124,
					16,
					17,
					198,
					175,
					196,
					22,
					46,
					255,
					254,
					234,
					11,
					103,
					0,
					1,
					76,
					12,
					3,
					0,
					4,
					98,
					66,
					74,
					3,
					103,
					0,
					1,
					64,
					82,
					0,
					66,
					135,
					30,
					0,
					31,
					174,
					255,
					255,
					113,
					160,
					175,
					179,
					12,
					0,
					0,
					10,
					102,
					0,
					1,
					42,
					12,
					56,
					0,
					54,
					175,
					183,
					102,
					16,
					78,
					185,
					0,
					255,
					132,
					20,
					74,
					56,
					175,
					207,
					103,
					10,
					96,
					0,
					1,
					38,
					78,
					185,
					0,
					255,
					130,
					184,
					66,
					0,
					116,
					1,
					96,
					0,
					1,
					4,
					66,
					56,
					175,
					172,
					22,
					56,
					175,
					183,
					12,
					3,
					0,
					160,
					102,
					44,
					22,
					56,
					175,
					181,
					188,
					3,
					103,
					8,
					12,
					3,
					0,
					254,
					102,
					0,
					0,
					228,
					17,
					252,
					0,
					224,
					175,
					197,
					17,
					252,
					0,
					170,
					175,
					198,
					17,
					252,
					0,
					5,
					175,
					206,
					78,
					185,
					0,
					255,
					135,
					114,
					96,
					0,
					0,
					200,
					12,
					3,
					0,
					161,
					102,
					14,
					78,
					185,
					0,
					255,
					130,
					174,
					66,
					56,
					175,
					183,
					96,
					0,
					0,
					180,
					12,
					3,
					0,
					40,
					102,
					12,
					17,
					252,
					0,
					104,
					175,
					197,
					66,
					56,
					175,
					198,
					96,
					202,
					12,
					3,
					0,
					39,
					102,
					28,
					17,
					252,
					0,
					103,
					175,
					197,
					17,
					252,
					0,
					1,
					175,
					198,
					66,
					56,
					175,
					199,
					66,
					56,
					175,
					200,
					17,
					252,
					0,
					7,
					175,
					206,
					96,
					174,
					12,
					3,
					0,
					52,
					102,
					36,
					17,
					252,
					0,
					1,
					176,
					16,
					17,
					252,
					0,
					116,
					175,
					197,
					66,
					56,
					175,
					198,
					17,
					252,
					0,
					74,
					175,
					199,
					17,
					252,
					0,
					6,
					175,
					206,
					78,
					185,
					0,
					255,
					135,
					114,
					96,
					106,
					12,
					3,
					0,
					60,
					102,
					66,
					17,
					252,
					0,
					124,
					175,
					197,
					17,
					252,
					0,
					20,
					175,
					198,
					52,
					124,
					64,
					136,
					54,
					18,
					12,
					67,
					165,
					160,
					102,
					6,
					52,
					124,
					64,
					28,
					96,
					4,
					52,
					124,
					96,
					28,
					17,
					210,
					175,
					199,
					82,
					138,
					17,
					210,
					175,
					200,
					82,
					138,
					17,
					210,
					175,
					201,
					82,
					138,
					17,
					210,
					175,
					202,
					17,
					252,
					0,
					9,
					175,
					206,
					96,
					0,
					255,
					62,
					12,
					3,
					0,
					63,
					102,
					6,
					66,
					129,
					66,
					56,
					175,
					207,
					66,
					0,
					78,
					185,
					0,
					255,
					144,
					96,
					74,
					2,
					103,
					0,
					254,
					90,
					74,
					56,
					175,
					178,
					103,
					0,
					254,
					82,
					76,
					223,
					3,
					199,
					78,
					94,
					78,
					117,
					78,
					185,
					0,
					255,
					131,
					78,
					71,
					248,
					175,
					194,
					69,
					248,
					175,
					160,
					118,
					5,
					52,
					219,
					81,
					203,
					255,
					252,
					20,
					219,
					78,
					185,
					0,
					255,
					130,
					224,
					66,
					56,
					175,
					172,
					78,
					117,
					49,
					249,
					0,
					255,
					130,
					64,
					246,
					12,
					78,
					117,
					78,
					86,
					255,
					252,
					72,
					231,
					96,
					0,
					66,
					130,
					34,
					2,
					82,
					129,
					12,
					129,
					0,
					4,
					124,
					229,
					99,
					14,
					116,
					1,
					78,
					185,
					0,
					255,
					143,
					244,
					17,
					194,
					175,
					207,
					96,
					88,
					29,
					120,
					246,
					14,
					255,
					252,
					78,
					185,
					0,
					255,
					144,
					96,
					78,
					185,
					0,
					255,
					144,
					96,
					78,
					185,
					0,
					255,
					144,
					96,
					22,
					46,
					255,
					252,
					234,
					11,
					103,
					48,
					61,
					120,
					246,
					14,
					255,
					254,
					29,
					110,
					255,
					254,
					255,
					252,
					16,
					46,
					255,
					255,
					116,
					1,
					22,
					46,
					255,
					252,
					234,
					11,
					12,
					3,
					0,
					7,
					103,
					12,
					12,
					3,
					0,
					5,
					103,
					6,
					12,
					3,
					0,
					6,
					102,
					6,
					49,
					252,
					0,
					1,
					175,
					174,
					74,
					2,
					103,
					148,
					74,
					2,
					102,
					4,
					16,
					60,
					0,
					255,
					76,
					223,
					0,
					6,
					78,
					94,
					78,
					117,
					64,
					231,
					0,
					124,
					7,
					0,
					78,
					185,
					0,
					255,
					130,
					214,
					78,
					185,
					0,
					255,
					142,
					198,
					70,
					223,
					78,
					117,
					47,
					0,
					78,
					185,
					0,
					255,
					144,
					210,
					78,
					185,
					0,
					255,
					145,
					6,
					12,
					64,
					1,
					152,
					101,
					6,
					12,
					64,
					2,
					131,
					99,
					12,
					16,
					57,
					0,
					255,
					130,
					41,
					78,
					185,
					0,
					255,
					144,
					14,
					32,
					31,
					78,
					117,
					47,
					0,
					48,
					60,
					39,
					15,
					78,
					185,
					0,
					255,
					130,
					106,
					78,
					185,
					0,
					255,
					144,
					96,
					78,
					185,
					0,
					255,
					144,
					196,
					74,
					0,
					102,
					12,
					16,
					57,
					0,
					255,
					130,
					46,
					78,
					185,
					0,
					255,
					144,
					14,
					32,
					31,
					78,
					117,
					78,
					86,
					255,
					252,
					72,
					231,
					227,
					204,
					66,
					56,
					176,
					48,
					116,
					1,
					17,
					194,
					176,
					16,
					74,
					56,
					176,
					16,
					103,
					10,
					66,
					56,
					176,
					16,
					78,
					185,
					0,
					255,
					141,
					160,
					32,
					56,
					176,
					18,
					12,
					128,
					0,
					0,
					54,
					176,
					99,
					6,
					116,
					1,
					17,
					194,
					176,
					59,
					78,
					185,
					0,
					255,
					133,
					164,
					74,
					56,
					176,
					16,
					102,
					0,
					2,
					252,
					74,
					128,
					102,
					122,
					66,
					120,
					176,
					54,
					78,
					185,
					0,
					255,
					141,
					208,
					74,
					56,
					176,
					16,
					102,
					106,
					12,
					56,
					0,
					2,
					176,
					34,
					101,
					68,
					12,
					120,
					74,
					252,
					176,
					144,
					103,
					14,
					16,
					57,
					0,
					255,
					130,
					49,
					78,
					185,
					0,
					255,
					144,
					14,
					96,
					76,
					112,
					1,
					17,
					192,
					176,
					40,
					66,
					56,
					176,
					48,
					74,
					56,
					176,
					50,
					102,
					60,
					78,
					185,
					0,
					255,
					136,
					62,
					78,
					185,
					0,
					255,
					144,
					122,
					78,
					185,
					0,
					255,
					136,
					104,
					78,
					185,
					0,
					255,
					139,
					236,
					17,
					192,
					176,
					50,
					96,
					30,
					78,
					185,
					0,
					255,
					136,
					62,
					78,
					185,
					0,
					255,
					144,
					122,
					78,
					185,
					0,
					255,
					136,
					104,
					78,
					185,
					0,
					255,
					139,
					236,
					17,
					252,
					0,
					1,
					176,
					50,
					44,
					56,
					176,
					18,
					34,
					56,
					176,
					30,
					146,
					134,
					12,
					129,
					0,
					0,
					8,
					0,
					101,
					4,
					50,
					60,
					8,
					0,
					36,
					1,
					226,
					74,
					32,
					124,
					0,
					255,
					160,
					0,
					36,
					72,
					66,
					64,
					118,
					1,
					42,
					3,
					186,
					66,
					98,
					14,
					208,
					82,
					84,
					138,
					82,
					67,
					101,
					6,
					42,
					2,
					186,
					67,
					100,
					242,
					209,
					120,
					176,
					54,
					56,
					124,
					0,
					1,
					49,
					204,
					176,
					56,
					74,
					56,
					176,
					48,
					103,
					106,
					74,
					56,
					176,
					58,
					102,
					100,
					12,
					134,
					0,
					0,
					56,
					0,
					99,
					92,
					32,
					6,
					52,
					60,
					64,
					0,
					148,
					64,
					34,
					2,
					220,
					184,
					176,
					22,
					45,
					70,
					255,
					252,
					78,
					185,
					0,
					255,
					144,
					96,
					74,
					56,
					176,
					16,
					102,
					62,
					31,
					56,
					176,
					148,
					63,
					56,
					176,
					146,
					32,
					1,
					67,
					238,
					255,
					252,
					78,
					185,
					0,
					255,
					143,
					82,
					42,
					124,
					0,
					2,
					0,
					0,
					33,
					205,
					176,
					22,
					66,
					184,
					176,
					18,
					77,
					25
				}, 0, 2062);
				Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
			}
		}

		private void WRITE512WRITEBOOTLOADB(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[1872]
				{
					135,
					78,
					109,
					16,
					240,
					54,
					0,
					7,
					66,
					255,
					137,
					238,
					4,
					184,
					0,
					2,
					0,
					0,
					176,
					30,
					124,
					1,
					17,
					198,
					176,
					58,
					50,
					60,
					8,
					0,
					146,
					66,
					226,
					74,
					82,
					66,
					49,
					194,
					176,
					56,
					38,
					56,
					176,
					22,
					214,
					184,
					176,
					18,
					45,
					67,
					255,
					252,
					74,
					56,
					176,
					16,
					102,
					38,
					78,
					185,
					0,
					255,
					144,
					96,
					31,
					56,
					176,
					148,
					63,
					56,
					176,
					146,
					32,
					1,
					67,
					238,
					255,
					252,
					48,
					120,
					176,
					56,
					65,
					240,
					131,
					176,
					0,
					255,
					159,
					254,
					78,
					185,
					0,
					255,
					143,
					82,
					2,
					129,
					0,
					0,
					255,
					255,
					210,
					184,
					176,
					18,
					33,
					193,
					176,
					18,
					12,
					129,
					0,
					0,
					54,
					176,
					99,
					6,
					17,
					252,
					0,
					1,
					176,
					59,
					36,
					56,
					176,
					30,
					178,
					130,
					100,
					38,
					74,
					56,
					176,
					16,
					102,
					32,
					66,
					64,
					78,
					185,
					0,
					255,
					130,
					106,
					16,
					57,
					0,
					255,
					130,
					40,
					78,
					185,
					0,
					255,
					130,
					126,
					78,
					185,
					0,
					255,
					135,
					114,
					78,
					185,
					0,
					255,
					133,
					164,
					178,
					130,
					100,
					8,
					74,
					56,
					176,
					16,
					103,
					0,
					254,
					190,
					74,
					56,
					176,
					16,
					102,
					0,
					1,
					50,
					74,
					120,
					176,
					54,
					103,
					12,
					16,
					57,
					0,
					255,
					130,
					44,
					78,
					185,
					0,
					255,
					144,
					14,
					74,
					56,
					176,
					48,
					103,
					100,
					38,
					56,
					176,
					26,
					34,
					3,
					32,
					57,
					0,
					255,
					130,
					36,
					144,
					65,
					49,
					192,
					176,
					52,
					8,
					248,
					0,
					7,
					176,
					146,
					34,
					60,
					0,
					74,
					252,
					0,
					2,
					184,
					255,
					0,
					0,
					255,
					176,
					146,
					131,
					184,
					176,
					146,
					82,
					131,
					45,
					67,
					255,
					252,
					78,
					185,
					0,
					255,
					144,
					96,
					74,
					56,
					176,
					16,
					102,
					28,
					31,
					56,
					176,
					148,
					63,
					56,
					176,
					146,
					48,
					56,
					176,
					52,
					67,
					238,
					255,
					252,
					32,
					124,
					0,
					255,
					160,
					0,
					78,
					185,
					0,
					255,
					143,
					82,
					8,
					184,
					0,
					7,
					176,
					146,
					49,
					252,
					74,
					252,
					176,
					144,
					22,
					56,
					176,
					34,
					182,
					56,
					176,
					35,
					102,
					92,
					38,
					56,
					176,
					26,
					34,
					3,
					32,
					57,
					0,
					255,
					130,
					32,
					144,
					65,
					49,
					192,
					176,
					52,
					8,
					248,
					0,
					7,
					176,
					146,
					2,
					184,
					255,
					0,
					0,
					255,
					176,
					146,
					0,
					184,
					0,
					74,
					252,
					0,
					176,
					146,
					82,
					131,
					45,
					67,
					255,
					252,
					78,
					185,
					0,
					255,
					144,
					96,
					74,
					56,
					176,
					16,
					102,
					28,
					31,
					56,
					176,
					148,
					63,
					56,
					176,
					146,
					48,
					56,
					176,
					52,
					67,
					238,
					255,
					252,
					32,
					124,
					0,
					255,
					160,
					0,
					78,
					185,
					0,
					255,
					143,
					82,
					8,
					184,
					0,
					7,
					176,
					146,
					66,
					184,
					176,
					18,
					74,
					56,
					176,
					40,
					103,
					50,
					74,
					56,
					176,
					41,
					103,
					44,
					74,
					56,
					176,
					42,
					103,
					38,
					74,
					56,
					176,
					43,
					103,
					32,
					74,
					56,
					176,
					44,
					103,
					26,
					74,
					56,
					176,
					45,
					103,
					20,
					74,
					56,
					176,
					46,
					103,
					14,
					74,
					56,
					176,
					47,
					103,
					8,
					17,
					252,
					0,
					1,
					176,
					38,
					96,
					18,
					16,
					57,
					0,
					255,
					130,
					40,
					78,
					185,
					0,
					255,
					130,
					126,
					78,
					185,
					0,
					255,
					135,
					114,
					74,
					56,
					176,
					38,
					103,
					0,
					252,
					208,
					16,
					57,
					0,
					255,
					130,
					45,
					78,
					185,
					0,
					255,
					144,
					14,
					76,
					223,
					51,
					199,
					78,
					94,
					78,
					117,
					72,
					231,
					128,
					128,
					22,
					56,
					176,
					34,
					12,
					3,
					0,
					1,
					102,
					0,
					1,
					32,
					49,
					252,
					255,
					255,
					176,
					144,
					32,
					124,
					0,
					6,
					0,
					0,
					78,
					185,
					0,
					255,
					141,
					58,
					78,
					185,
					0,
					255,
					144,
					136,
					112,
					120,
					78,
					185,
					0,
					255,
					130,
					126,
					78,
					185,
					0,
					255,
					135,
					114,
					78,
					185,
					0,
					255,
					144,
					96,
					48,
					60,
					39,
					15,
					78,
					185,
					0,
					255,
					130,
					106,
					78,
					185,
					0,
					255,
					144,
					96,
					78,
					185,
					0,
					255,
					144,
					122,
					78,
					185,
					0,
					255,
					136,
					104,
					32,
					124,
					0,
					4,
					0,
					0,
					78,
					185,
					0,
					255,
					141,
					58,
					78,
					185,
					0,
					255,
					144,
					136,
					112,
					120,
					78,
					185,
					0,
					255,
					130,
					126,
					78,
					185,
					0,
					255,
					135,
					114,
					78,
					185,
					0,
					255,
					144,
					96,
					48,
					60,
					39,
					15,
					78,
					185,
					0,
					255,
					130,
					106,
					78,
					185,
					0,
					255,
					144,
					96,
					78,
					185,
					0,
					255,
					144,
					122,
					78,
					185,
					0,
					255,
					136,
					104,
					32,
					124,
					0,
					2,
					0,
					0,
					78,
					185,
					0,
					255,
					141,
					58,
					78,
					185,
					0,
					255,
					144,
					136,
					112,
					120,
					78,
					185,
					0,
					255,
					130,
					126,
					78,
					185,
					0,
					255,
					135,
					114,
					78,
					185,
					0,
					255,
					144,
					96,
					48,
					60,
					39,
					15,
					78,
					185,
					0,
					255,
					130,
					106,
					78,
					185,
					0,
					255,
					144,
					96,
					78,
					185,
					0,
					255,
					144,
					122,
					78,
					185,
					0,
					255,
					136,
					104,
					32,
					124,
					0,
					0,
					128,
					0,
					78,
					185,
					0,
					255,
					141,
					58,
					78,
					185,
					0,
					255,
					144,
					136,
					112,
					120,
					78,
					185,
					0,
					255,
					130,
					126,
					78,
					185,
					0,
					255,
					135,
					114,
					78,
					185,
					0,
					255,
					144,
					96,
					48,
					60,
					39,
					15,
					78,
					185,
					0,
					255,
					130,
					106,
					78,
					185,
					0,
					255,
					144,
					96,
					78,
					185,
					0,
					255,
					144,
					122,
					78,
					185,
					0,
					255,
					136,
					104,
					66,
					56,
					176,
					59,
					48,
					124,
					0,
					0,
					78,
					185,
					0,
					255,
					141,
					58,
					96,
					20,
					12,
					3,
					0,
					2,
					101,
					14,
					12,
					3,
					0,
					8,
					98,
					8,
					32,
					124,
					0,
					0,
					128,
					0,
					96,
					228,
					78,
					185,
					0,
					255,
					144,
					136,
					76,
					223,
					1,
					1,
					78,
					117,
					78,
					86,
					255,
					252,
					47,
					0,
					48,
					185,
					0,
					255,
					130,
					20,
					48,
					185,
					0,
					255,
					130,
					28,
					78,
					185,
					0,
					255,
					144,
					96,
					61,
					80,
					255,
					252,
					61,
					110,
					255,
					252,
					255,
					254,
					8,
					46,
					0,
					7,
					255,
					255,
					103,
					232,
					48,
					185,
					0,
					255,
					130,
					30,
					61,
					110,
					255,
					252,
					255,
					254,
					8,
					46,
					0,
					5,
					255,
					255,
					103,
					34,
					61,
					110,
					255,
					252,
					255,
					254,
					8,
					46,
					0,
					3,
					255,
					255,
					103,
					8,
					16,
					57,
					0,
					255,
					130,
					42,
					96,
					6,
					16,
					57,
					0,
					255,
					130,
					43,
					78,
					185,
					0,
					255,
					144,
					14,
					32,
					31,
					78,
					94,
					78,
					117,
					66,
					184,
					176,
					18,
					8,
					184,
					0,
					7,
					176,
					146,
					118,
					1,
					66,
					132,
					24,
					3,
					66,
					48,
					65,
					160,
					176,
					39,
					82,
					3,
					12,
					3,
					0,
					8,
					99,
					238,
					66,
					56,
					176,
					49,
					66,
					56,
					176,
					58,
					66,
					56,
					176,
					38,
					66,
					56,
					176,
					50,
					78,
					117,
					72,
					231,
					224,
					128,
					32,
					124,
					0,
					255,
					160,
					0,
					49,
					208,
					176,
					36,
					84,
					136,
					54,
					16,
					40,
					3,
					17,
					196,
					176,
					34,
					12,
					67,
					0,
					2,
					101,
					80,
					12,
					67,
					0,
					8,
					98,
					74,
					216,
					4,
					2,
					68,
					0,
					255,
					33,
					240,
					69,
					160,
					176,
					64,
					176,
					22,
					33,
					240,
					69,
					160,
					176,
					68,
					176,
					26,
					74,
					56,
					176,
					49,
					102,
					126,
					66,
					128,
					120,
					2,
					34,
					4,
					36,
					1,
					212,
					2,
					2,
					66,
					0,
					255,
					38,
					48,
					37,
					160,
					176,
					68,
					182,
					128,
					99,
					6,
					32,
					3,
					17,
					193,
					176,
					35,
					82,
					68,
					12,
					68,
					0,
					8,
					99,
					222,
					17,
					252,
					0,
					1,
					176,
					49,
					96,
					80,
					34,
					60,
					0,
					255,
					160,
					0,
					6,
					129,
					0,
					0,
					5,
					0,
					33,
					193,
					176,
					136,
					32,
					65,
					69,
					248,
					176,
					60,
					118,
					37,
					52,
					216,
					81,
					203,
					255,
					252,
					49,
					248,
					176,
					60,
					176,
					36,
					54,
					56,
					176,
					62,
					17,
					195,
					176,
					34,
					114,
					1,
					17,
					193,
					176,
					48,
					178,
					3,
					103,
					14,
					16,
					57,
					0,
					255,
					130,
					49,
					78,
					185,
					0,
					255,
					144,
					14,
					96,
					12,
					33,
					248,
					176,
					72,
					176,
					22,
					33,
					248,
					176,
					76,
					176,
					26,
					38,
					56,
					176,
					26,
					150,
					184,
					176,
					22,
					82,
					131,
					33,
					195,
					176,
					30,
					66,
					67,
					22,
					56,
					176,
					34,
					74,
					48,
					49,
					160,
					176,
					39,
					103,
					14,
					16,
					57,
					0,
					255,
					130,
					49,
					78,
					185,
					0,
					255,
					144,
					14,
					96,
					8,
					31,
					188,
					0,
					1,
					49,
					160,
					176,
					39,
					76,
					223,
					1,
					7,
					78,
					117,
					72,
					231,
					128,
					128,
					17,
					252,
					0,
					1,
					176,
					59,
					66,
					56,
					176,
					16,
					48,
					124,
					5,
					0,
					33,
					200,
					176,
					136,
					69,
					248,
					176,
					60,
					120,
					37,
					52,
					216,
					81,
					204,
					255,
					252,
					36,
					124,
					0,
					7,
					255,
					254,
					49,
					210,
					176,
					144,
					49,
					249,
					0,
					255,
					130,
					56,
					250,
					76,
					49,
					249,
					0,
					255,
					130,
					50,
					250,
					74,
					49,
					249,
					0,
					255,
					130,
					52,
					250,
					78,
					52,
					124,
					0,
					0,
					52,
					185,
					0,
					255,
					130,
					26,
					56,
					18,
					49,
					196,
					176,
					142,
					52,
					124,
					0,
					2,
					54,
					18,
					49,
					195,
					176,
					140,
					52,
					124,
					0,
					0,
					52,
					185,
					0,
					255,
					130,
					30,
					12,
					68,
					0,
					137,
					102,
					6,
					12,
					67,
					68,
					113,
					103,
					12,
					16,
					57,
					0,
					255,
					130,
					48,
					78,
					185,
					0,
					255,
					144,
					14,
					78,
					185,
					0,
					255,
					136,
					148,
					76,
					223,
					1,
					1,
					78,
					117,
					78,
					86,
					255,
					252,
					72,
					231,
					96,
					0,
					78,
					185,
					0,
					255,
					144,
					122,
					78,
					185,
					0,
					255,
					136,
					104,
					52,
					0,
					114,
					1,
					12,
					66,
					0,
					1,
					96,
					114,
					36,
					81,
					52,
					185,
					0,
					255,
					130,
					22,
					8,
					46,
					0,
					7,
					0,
					8,
					102,
					4,
					54,
					16,
					96,
					6,
					38,
					46,
					0,
					8,
					224,
					139,
					36,
					81,
					52,
					131,
					36,
					81,
					52,
					185,
					0,
					255,
					130,
					24,
					78,
					185,
					0,
					255,
					144,
					96,
					36,
					81,
					61,
					82,
					255,
					252,
					61,
					110,
					255,
					252,
					255,
					254,
					8,
					46,
					0,
					7,
					255,
					255,
					103,
					230,
					61,
					110,
					255,
					252,
					255,
					254,
					8,
					46,
					0,
					4,
					255,
					255,
					103,
					12,
					16,
					57,
					0,
					255,
					130,
					44,
					78,
					185,
					0,
					255,
					144,
					14,
					36,
					81,
					52,
					185,
					0,
					255,
					130,
					30,
					84,
					136,
					36,
					73,
					38,
					18,
					84,
					131,
					36,
					131,
					84,
					65,
					101,
					4,
					180,
					65,
					100,
					140,
					78,
					185,
					0,
					255,
					144,
					136,
					76,
					223,
					0,
					6,
					78,
					94,
					78,
					116,
					0,
					4,
					17,
					252,
					0,
					1,
					176,
					16,
					74,
					56,
					176,
					59,
					103,
					6,
					78,
					185,
					0,
					255,
					130,
					100,
					78,
					185,
					0,
					255,
					141,
					160,
					78,
					117,
					47,
					1,
					18,
					0,
					22,
					57,
					0,
					255,
					130,
					45,
					178,
					3,
					102,
					16,
					78,
					185,
					0,
					255,
					144,
					136,
					74,
					0,
					103,
					6,
					18,
					57,
					0,
					255,
					130,
					47,
					32,
					1,
					78,
					185,
					0,
					255,
					130,
					126,
					78,
					185,
					0,
					255,
					135,
					114,
					78,
					185,
					0,
					255,
					144,
					96,
					48,
					60,
					39,
					15,
					78,
					185,
					0,
					255,
					130,
					106,
					78,
					185,
					0,
					255,
					144,
					96,
					78,
					185,
					0,
					255,
					135,
					150,
					78,
					185,
					0,
					255,
					143,
					244,
					34,
					31,
					78,
					117,
					17,
					252,
					0,
					85,
					250,
					39,
					17,
					252,
					0,
					170,
					250,
					39,
					8,
					184,
					0,
					7,
					208,
					6,
					8,
					248,
					0,
					7,
					208,
					6,
					78,
					117,
					54,
					56,
					226,
					250,
					8,
					195,
					0,
					0,
					49,
					195,
					226,
					250,
					78,
					117,
					47,
					1,
					54,
					56,
					226,
					250,
					8,
					131,
					0,
					0,
					49,
					195,
					226,
					250,
					114,
					1,
					78,
					185,
					0,
					255,
					144,
					96,
					48,
					60,
					39,
					15,
					78,
					185,
					0,
					255,
					130,
					106,
					82,
					65,
					12,
					65,
					0,
					15,
					99,
					232,
					78,
					185,
					0,
					255,
					144,
					196,
					74,
					0,
					103,
					4,
					112,
					1,
					96,
					2,
					66,
					0,
					34,
					31,
					78,
					117,
					54,
					56,
					225,
					18,
					8,
					3,
					0,
					8,
					86,
					192,
					68,
					0,
					78,
					117,
					49,
					249,
					0,
					255,
					130,
					74,
					242,
					8,
					49,
					249,
					0,
					255,
					130,
					72,
					242,
					0,
					49,
					249,
					0,
					255,
					130,
					76,
					242,
					10,
					49,
					249,
					0,
					255,
					130,
					78,
					242,
					12,
					49,
					249,
					0,
					255,
					130,
					80,
					242,
					14,
					49,
					252,
					0,
					6,
					242,
					48,
					66,
					120,
					242,
					16,
					78,
					117,
					8,
					248,
					0,
					5,
					242,
					12,
					112,
					106,
					78,
					185,
					0,
					255,
					130,
					106,
					8,
					56,
					0,
					7,
					242,
					16,
					102,
					4,
					66,
					64,
					96,
					10,
					8,
					184,
					0,
					7,
					242,
					16,
					48,
					56,
					242,
					176,
					78,
					117,
					78,
					86,
					0,
					0,
					227,
					64
				}, 0, 1872);
				Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
			}
		}

		private void WRITECALMODULES(object sender, EventArgs e)
		{
			if (cancel != 0 || !serialPort1.IsOpen)
			{
				return;
			}
			x = 0;
			y = 12;
			while (x < moduleMessageSize)
			{
				vpwmessagetx2062[y] = calfile[calfileoffset];
				x++;
				y++;
				calfileoffset++;
			}
			while (y < 2062)
			{
				vpwmessagetx2062[y] = 0;
				y++;
			}
			vpwmessagetx2062[2060] = 0;
			vpwmessagetx2062[2061] = 0;
			x = 0;
			chksumlsb = 0;
			chksummsb = 0;
			byte[] array = vpwmessagetx2062;
			foreach (byte b in array)
			{
				if (x < 7)
				{
					x++;
				}
				else
				{
					if (x < 7)
					{
						continue;
					}
					chksumlsb += b;
					if (chksumlsb > 255)
					{
						chksummsb++;
						chksumlsb -= 256;
						if (chksummsb > 255)
						{
							chksummsb -= 256;
						}
					}
					x++;
				}
			}
			vpwmessagetx2062[2060] = Convert.ToByte(chksummsb);
			vpwmessagetx2062[2061] = Convert.ToByte(chksumlsb);
			serialPort1.DiscardInBuffer();
			serialPort1.Write(vpwmessagetx2062, 0, 2062);
			Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
			if (vpwmessagerx7[5] != 115)
			{
				if (vpwmessagerx7[5] == 134)
				{
					calfileoffset = 98304;
					progressBar1.Value = 98304;
					textBox1.AppendText("Module 7 Write Success");
					textBox1.AppendText("\n");
					MessageBox.Show(this, "Calibration Write Success", "Calibration Write Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else if ((vpwmessagerx7[5] != 115) & (vpwmessagerx7[5] != 134))
				{
					MessageBox.Show(this, "Block Write Fail", "Block Write Fail", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					cancel = 1;
				}
			}
		}

		private void WRITEOSBLOCK0X800(object sender, EventArgs e)
		{
			if (cancel != 0 || !serialPort1.IsOpen)
			{
				return;
			}
			x = 0;
			y = 12;
			if (comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B")
			{
				while (x < 2048)
				{
					vpwmessagetx2062[y] = osfile512[osfileoffset];
					x++;
					y++;
					osfileoffset++;
				}
			}
			if (comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B")
			{
				while (x < 2048)
				{
					vpwmessagetx2062[y] = osfile1024[osfileoffset];
					x++;
					y++;
					osfileoffset++;
				}
			}
			vpwmessagetx2062[2060] = 0;
			vpwmessagetx2062[2061] = 0;
			x = 0;
			chksumlsb = 0;
			chksummsb = 0;
			byte[] array = vpwmessagetx2062;
			foreach (byte b in array)
			{
				if (x < 7)
				{
					x++;
				}
				else
				{
					if (x < 7)
					{
						continue;
					}
					chksumlsb += b;
					if (chksumlsb > 255)
					{
						chksummsb++;
						chksumlsb -= 256;
						if (chksummsb > 255)
						{
							chksummsb -= 256;
						}
					}
					x++;
				}
			}
			vpwmessagetx2062[2060] = Convert.ToByte(chksummsb);
			vpwmessagetx2062[2061] = Convert.ToByte(chksumlsb);
			serialPort1.DiscardInBuffer();
			serialPort1.Write(vpwmessagetx2062, 0, 2062);
			Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCEB));
		}

		private void WRITESECURITYBLOCK0X50(object sender, EventArgs e)
		{
			if (cancel != 0 || !serialPort1.IsOpen)
			{
				return;
			}
			securitywritebytes[92] = 0;
			securitywritebytes[93] = 0;
			x = 0;
			chksumlsb = 0;
			chksummsb = 0;
			byte[] array = securitywritebytes;
			foreach (byte b in array)
			{
				if (x < 7)
				{
					x++;
				}
				else
				{
					if (x < 7)
					{
						continue;
					}
					chksumlsb += b;
					if (chksumlsb > 255)
					{
						chksummsb++;
						chksumlsb -= 256;
						if (chksummsb > 255)
						{
							chksummsb -= 256;
						}
					}
					x++;
				}
			}
			securitywritebytes[92] = Convert.ToByte(chksummsb);
			securitywritebytes[93] = Convert.ToByte(chksumlsb);
			serialPort1.DiscardInBuffer();
			serialPort1.Write(securitywritebytes, 0, 94);
			Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCEB));
			if (vpwmessagerx7[5] == 115)
			{
				textBox1.AppendText("Security Write Success");
				textBox1.AppendText("\n");
			}
			else if (vpwmessagerx7[5] != 115)
			{
				textBox1.AppendText("Security Write Fail");
				textBox1.AppendText("\n");
			}
		}

		private void WRITESEEDKEYBLOCK0X4(object sender, EventArgs e)
		{
			if (cancel != 0 || !serialPort1.IsOpen)
			{
				return;
			}
			securitywritebytes[92] = 0;
			securitywritebytes[93] = 0;
			x = 0;
			chksumlsb = 0;
			chksummsb = 0;
			byte[] array = securitywritebytes;
			foreach (byte b in array)
			{
				if (x < 7)
				{
					x++;
				}
				else
				{
					if (x < 7)
					{
						continue;
					}
					chksumlsb += b;
					if (chksumlsb > 255)
					{
						chksummsb++;
						chksumlsb -= 256;
						if (chksummsb > 255)
						{
							chksummsb -= 256;
						}
					}
					x++;
				}
			}
			securitywritebytes[92] = Convert.ToByte(chksummsb);
			securitywritebytes[93] = Convert.ToByte(chksumlsb);
			serialPort1.DiscardInBuffer();
			serialPort1.Write(securitywritebytes, 0, 94);
			Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCEB));
		}

		private void WRITESERIALNUMBERBLOCK0X1A(object sender, EventArgs e)
		{
			if (cancel != 0 || !serialPort1.IsOpen)
			{
				return;
			}
			serialnowritebytes[24] = 0;
			serialnowritebytes[25] = 0;
			x = 0;
			chksumlsb = 0;
			chksummsb = 0;
			byte[] array = serialnowritebytes;
			foreach (byte b in array)
			{
				if (x < 7)
				{
					x++;
				}
				else
				{
					if (x < 7)
					{
						continue;
					}
					chksumlsb += b;
					if (chksumlsb > 255)
					{
						chksummsb++;
						chksumlsb -= 256;
						if (chksummsb > 255)
						{
							chksummsb -= 256;
						}
					}
					x++;
				}
			}
			serialnowritebytes[24] = Convert.ToByte(chksummsb);
			serialnowritebytes[25] = Convert.ToByte(chksumlsb);
			serialPort1.DiscardInBuffer();
			serialPort1.Write(serialnowritebytes, 0, 26);
			Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCEB));
		}

		private void BOOTFROMRAM(object sender, EventArgs e)
		{
			if (cancel == 0)
			{
				if (serialPort1.IsOpen)
				{
					serialPort1.DiscardInBuffer();
					serialPort1.Write(new byte[14]
					{
						128,
						12,
						109,
						16,
						240,
						54,
						128,
						0,
						0,
						255,
						145,
						62,
						2,
						78
					}, 0, 14);
					Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
				}
				if (vpwmessagerx7[5] == 115)
				{
					textBox1.AppendText("Ram Boot Success");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				if (vpwmessagerx7[5] != 115)
				{
					textBox1.AppendText("Ram Boot Fail");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
					cancel = 1;
				}
			}
		}

		private void BOOTFROMRAM2(object sender, EventArgs e)
		{
			if (cancel == 0)
			{
				if (serialPort1.IsOpen)
				{
					serialPort1.DiscardInBuffer();
					serialPort1.Write(new byte[14]
					{
						128,
						12,
						109,
						16,
						240,
						54,
						128,
						0,
						0,
						255,
						129,
						238,
						2,
						238
					}, 0, 14);
					Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
				}
				if (vpwmessagerx7[5] == 115)
				{
					textBox1.AppendText("Ram Boot Success");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				if (vpwmessagerx7[5] != 115)
				{
					textBox1.AppendText("Ram Boot Fail");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
					cancel = 1;
				}
			}
		}

		private void BOOTFROMRAM3(object sender, EventArgs e)
		{
			if (cancel == 0)
			{
				if (serialPort1.IsOpen)
				{
					serialPort1.DiscardInBuffer();
					serialPort1.Write(new byte[14]
					{
						128,
						12,
						109,
						16,
						240,
						54,
						128,
						0,
						0,
						255,
						129,
						254,
						2,
						254
					}, 0, 14);
					Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCE));
				}
				if (vpwmessagerx7[5] == 115)
				{
					textBox1.AppendText("Ram Boot Success");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				if (vpwmessagerx7[5] != 115)
				{
					textBox1.AppendText("Ram Boot Fail");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
					cancel = 1;
				}
			}
		}

		private void READ800BYTES(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				SerialPort serialPort = serialPort1;
				byte[] array = new byte[12]
				{
					128,
					10,
					108,
					16,
					240,
					53,
					1,
					8,
					0,
					0,
					0,
					0
				};
				array[9] = readaddress[0];
				array[10] = readaddress[1];
				serialPort.Write(array, 0, 12);
				Invoke(new EventHandler(LISTENFORVPW2068BYTERESPONCE));
			}
		}

		private void READ1000BYTES(object sender, EventArgs e)
		{
			if (cancel == 0 && serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				SerialPort serialPort = serialPort1;
				byte[] array = new byte[12]
				{
					128,
					10,
					108,
					16,
					240,
					53,
					1,
					16,
					0,
					0,
					0,
					0
				};
				array[9] = readaddress[0];
				array[10] = readaddress[1];
				serialPort.Write(array, 0, 12);
				Invoke(new EventHandler(LISTENFORVPW4116BYTERESPONCE));
			}
		}

		private void SECURITYTIMEOUT(object sender, EventArgs e)
		{
			Thread.Sleep(7500);
			Thread.Sleep(7500);
		}

		private void LISTENFORVPW5BYTERESPONCE(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			a = 0;
			b = 0;
			if (serialPort1.BytesToRead < 5)
			{
				while (a < 5)
				{
					a = serialPort1.BytesToRead;
					Thread.Sleep(1);
					b++;
					if (b == 300)
					{
						textBox1.AppendText("NO RESPONCE");
						textBox1.AppendText("\n");
						cancel = 1;
						return;
					}
				}
			}
			serialPort1.Read(vpwmessagerx5, 0, 5);
			buffer0 = ByteArrayToHexString(new byte[5]
			{
				vpwmessagerx5[0],
				vpwmessagerx5[1],
				vpwmessagerx5[2],
				vpwmessagerx5[3],
				vpwmessagerx5[4]
			});
			textBox1.AppendText(buffer0);
			textBox1.AppendText("\n");
		}

		private void LISTENFORVPW6BYTERESPONCE(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			a = 0;
			b = 0;
			if (serialPort1.BytesToRead < 6)
			{
				while (a < 6)
				{
					a = serialPort1.BytesToRead;
					Thread.Sleep(1);
					b++;
					if (b == 300)
					{
						textBox1.AppendText("NO RESPONCE");
						textBox1.AppendText("\n");
						return;
					}
				}
			}
			serialPort1.Read(vpwmessagerx6, 0, 6);
			buffer0 = ByteArrayToHexString(vpwmessagerx6);
			textBox1.AppendText(buffer0);
			textBox1.AppendText("\n");
		}

		private void LISTENFORVPW7BYTERESPONCE(object sender, EventArgs e)
		{
			vpwmessagerx7[5] = 0;
			if (!serialPort1.IsOpen)
			{
				return;
			}
			a = 0;
			b = 0;
			while (a < 7)
			{
				a = serialPort1.BytesToRead;
				Thread.Sleep(5);
				b++;
				if (b == 500)
				{
					textBox1.AppendText("NO RESPONCE");
					textBox1.AppendText("\n");
					cancel = 1;
					return;
				}
			}
			serialPort1.Read(vpwmessagerx7, 0, 7);
			buffer0 = ByteArrayToHexString(vpwmessagerx7);
			textBox1.AppendText(buffer0);
			textBox1.AppendText("\n");
		}

		private void LISTENFORVPW7BYTERESPONCEB(object sender, EventArgs e)
		{
			vpwmessagerx7[5] = 0;
			if (!serialPort1.IsOpen)
			{
				return;
			}
			a = 0;
			b = 0;
			while (a < 7)
			{
				a = serialPort1.BytesToRead;
				Thread.Sleep(5);
				b++;
				if (b == 500)
				{
					textBox1.AppendText("NO RESPONCE");
					textBox1.AppendText("\n");
					cancel = 1;
					return;
				}
			}
			serialPort1.Read(vpwmessagerx7, 0, 7);
			buffer0 = ByteArrayToHexString(vpwmessagerx7);
			textBox1.AppendText(buffer0);
			textBox1.AppendText("\n");
			if (vpwmessagerx7[5] == 115)
			{
				textBox1.AppendText("Block Write Ok");
				textBox1.AppendText("\n");
				return;
			}
			if (vpwmessagerx7[5] == 120)
			{
				textBox1.AppendText("Block Erase Ok");
				textBox1.AppendText("\n");
				Thread.Sleep(5);
				Invoke(new EventHandler(TESTERPRESENTMESSAGE));
				Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCEB));
			}
			if ((vpwmessagerx7[5] != 115) & (vpwmessagerx7[5] != 120))
			{
				textBox1.AppendText("Block Write Fail");
				textBox1.AppendText("\n");
			}
		}

		private void LISTENFORVPW8BYTERESPONCE(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			a = 0;
			b = 0;
			if (serialPort1.BytesToRead < 8)
			{
				while (a < 8)
				{
					a = serialPort1.BytesToRead;
					Thread.Sleep(1);
					b++;
					if (b == 300)
					{
						textBox1.AppendText("NO RESPONCE");
						textBox1.AppendText("\n");
						return;
					}
				}
			}
			serialPort1.Read(vpwmessagerx8, 0, 8);
			buffer0 = ByteArrayToHexString(vpwmessagerx8);
			textBox1.AppendText(buffer0);
			textBox1.AppendText("\n");
		}

		private void LISTENFORVPW10BYTERESPONCE(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			a = 0;
			b = 0;
			if (serialPort1.BytesToRead < 10)
			{
				while (a < 10)
				{
					a = serialPort1.BytesToRead;
					Thread.Sleep(1);
					b++;
					if (b == 300)
					{
						textBox1.AppendText("NO RESPONCE");
						textBox1.AppendText("\n");
						cancel = 1;
						return;
					}
				}
			}
			serialPort1.Read(vpwmessagerx10, 0, 10);
			buffer0 = ByteArrayToHexString(new byte[5]
			{
				vpwmessagerx10[0],
				vpwmessagerx10[1],
				vpwmessagerx10[2],
				vpwmessagerx10[3],
				vpwmessagerx10[4]
			});
			textBox1.AppendText(buffer0);
			textBox1.AppendText("\n");
		}

		private void LISTENFORFAULTCODERESPONCE(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			a = 0;
			b = 0;
			if (serialPort1.BytesToRead < 8)
			{
				while (a < 8)
				{
					a = serialPort1.BytesToRead;
					Thread.Sleep(1);
					b++;
					if (b == 300)
					{
						textBox1.AppendText("NO RESPONCE");
						textBox1.AppendText("\n");
						return;
					}
				}
			}
			serialPort1.Read(vpwmessagerx8, 0, 8);
			buffer0 = ByteArrayToHexString(vpwmessagerx8);
			if ((vpwmessagerx8[4] == 0) & (vpwmessagerx8[5] == 0))
			{
				textBox1.AppendText("P0000");
				textBox1.AppendText("\n");
				textBox1.AppendText("All PCM Codes Read");
				textBox1.AppendText("\n");
			}
			if (vpwmessagerx8[4] != 0 || vpwmessagerx8[5] != 0)
			{
				CodePrefix = "P";
				if (buffer0[12] == 'D')
				{
					CodePrefix = "U";
					textBox1.AppendText(CodePrefix + "1" + buffer0[13] + buffer0[15] + buffer0[16]);
					textBox1.AppendText("\n");
					Invoke(new EventHandler(LISTENFORFAULTCODERESPONCE));
				}
				else
				{
					textBox1.AppendText(CodePrefix + buffer0[12] + buffer0[13] + buffer0[15] + buffer0[16]);
					textBox1.AppendText("\n");
					Invoke(new EventHandler(LISTENFORFAULTCODERESPONCE));
				}
			}
		}

		private void LISTENFORSILENCE(object sender, EventArgs e)
		{
			if (serialPort1.IsOpen)
			{
				serialPort1.DiscardInBuffer();
				Thread.Sleep(2);
				if (serialPort1.BytesToRead > 0)
				{
					Invoke(new EventHandler(LISTENFORSILENCE));
				}
			}
		}

		private void LISTENFORPROGRAMPROMPT(object sender, EventArgs e)
		{
			if (serialPort1.IsOpen)
			{
				vpwmessagerx[2] = 0;
				vpwmessagerx[3] = 0;
				vpwmessagerx[4] = 0;
				serialPort1.DiscardInBuffer();
				Thread.Sleep(800);
				if (serialPort1.BytesToRead > 5)
				{
					serialPort1.Read(vpwmessagerx, 0, 6);
					buffer0 = ByteArrayToHexString(new byte[6]
					{
						vpwmessagerx[0],
						vpwmessagerx[1],
						vpwmessagerx[2],
						vpwmessagerx[3],
						vpwmessagerx[4],
						vpwmessagerx[5]
					});
				}
				if ((vpwmessagerx[2] == 16) & (vpwmessagerx[3] == 162) & (vpwmessagerx[4] == 0))
				{
					textBox1.AppendText(buffer0);
					textBox1.AppendText("\n");
					textBox1.AppendText("Program Prompt Mode");
					textBox1.AppendText("\n");
					textBox1.AppendText("PCM Request Write Bin");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				if ((vpwmessagerx[2] == 16) & (vpwmessagerx[3] == 162) & (vpwmessagerx[4] == 1))
				{
					textBox1.AppendText(buffer0);
					textBox1.AppendText("\n");
					textBox1.AppendText("Program Prompt Mode");
					textBox1.AppendText("\n");
					textBox1.AppendText("PCM Request Write Cal");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
			}
		}

		private void LISTENFORVPW12BYTERESPONCE(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			a = 0;
			b = 0;
			if (serialPort1.BytesToRead < 12)
			{
				while (a < 12)
				{
					a = serialPort1.BytesToRead;
					Thread.Sleep(1);
					b++;
					if (b == 300)
					{
						textBox1.AppendText("NO RESPONCE");
						textBox1.AppendText("\n");
						return;
					}
				}
			}
			serialPort1.Read(vpwmessagerx, 0, 12);
			buffer0 = ByteArrayToHexString(new byte[5]
			{
				vpwmessagerx[0],
				vpwmessagerx[1],
				vpwmessagerx[2],
				vpwmessagerx[3],
				vpwmessagerx[4]
			});
			textBox1.AppendText(buffer0);
			textBox1.AppendText("\n");
		}

		private void LISTENFORVPW2068BYTERESPONCE(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			a = 0;
			b = 0;
			while (a < 2068)
			{
				a = serialPort1.BytesToRead;
				Thread.Sleep(5);
				b++;
				if (b == 800)
				{
					textBox1.AppendText("NO RESPONCE");
					textBox1.AppendText("\n");
					cancel = 1;
					return;
				}
			}
			serialPort1.Read(vpwmessagerx17, 0, 17);
			buffer1 = ByteArrayToHexString(new byte[6]
			{
				vpwmessagerx17[0],
				vpwmessagerx17[1],
				vpwmessagerx17[2],
				vpwmessagerx17[3],
				vpwmessagerx17[4],
				vpwmessagerx17[5]
			});
			buffer0 = ByteArrayToHexString(new byte[4]
			{
				vpwmessagerx17[13],
				vpwmessagerx17[14],
				vpwmessagerx17[15],
				vpwmessagerx17[16]
			});
			textBox1.AppendText(buffer1);
			textBox1.AppendText("\n");
			textBox1.AppendText("Address 0x " + buffer0);
			textBox1.AppendText("\n");
			serialPort1.Read(binread, binoffset, 2048);
			binoffset += 2048;
			x = readaddress[0] * 256 + readaddress[1];
			x += 8;
			for (y = 0; x > 255; x -= 256)
			{
				y++;
			}
			readaddress[0] = Convert.ToByte(y);
			readaddress[1] = Convert.ToByte(x);
		}

		private void LISTENFORVPW4116BYTERESPONCE(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			a = 0;
			b = 0;
			if (serialPort1.BytesToRead < 4116)
			{
				while (a < 4116)
				{
					a = serialPort1.BytesToRead;
					Thread.Sleep(1);
					b++;
					if (b == 300)
					{
						textBox1.AppendText("NO RESPONCE");
						textBox1.AppendText("\n");
						return;
					}
				}
			}
			serialPort1.Read(vpwmessagerx17, 0, 17);
			buffer1 = ByteArrayToHexString(new byte[6]
			{
				vpwmessagerx17[0],
				vpwmessagerx17[1],
				vpwmessagerx17[2],
				vpwmessagerx17[3],
				vpwmessagerx17[4],
				vpwmessagerx17[5]
			});
			buffer0 = ByteArrayToHexString(new byte[4]
			{
				vpwmessagerx17[13],
				vpwmessagerx17[14],
				vpwmessagerx17[15],
				vpwmessagerx17[16]
			});
			textBox1.AppendText(buffer1);
			textBox1.AppendText("\n");
			textBox1.AppendText("Address 0x " + buffer0);
			textBox1.AppendText("\n");
			serialPort1.Read(binread, binoffset, 4096);
			binoffset += 4096;
			x = readaddress[0] * 256 + readaddress[1];
			x += 16;
			for (y = 0; x > 255; x -= 256)
			{
				y++;
			}
			readaddress[0] = Convert.ToByte(y);
			readaddress[1] = Convert.ToByte(x);
		}

		private void LISTENFORPCMSEEDRESPONCE(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			Thread.Sleep(50);
			x = serialPort1.BytesToRead;
			if (x != 8 && x != 7)
			{
				cancel = 1;
				textBox1.AppendText("Seed Responce Error");
				textBox1.AppendText("\n");
			}
			else if (serialPort1.BytesToRead == 8)
			{
				serialPort1.Read(vpwmessagerx8, 0, 8);
				buffer0 = ByteArrayToHexString(new byte[5]
				{
					vpwmessagerx8[0],
					vpwmessagerx8[1],
					vpwmessagerx8[2],
					vpwmessagerx8[3],
					vpwmessagerx8[4]
				});
				textBox1.AppendText(buffer0);
				textBox1.AppendText("\n");
			}
			else if (serialPort1.BytesToRead == 7)
			{
				serialPort1.Read(vpwmessagerx7, 0, 6);
				buffer0 = ByteArrayToHexString(vpwmessagerx7);
				textBox1.AppendText(buffer0);
				textBox1.AppendText("\n");
				if (vpwmessagerx7[5] == 55)
				{
					textBox1.AppendText("PCM Security Timeout");
					textBox1.AppendText("\n");
					textBox1.AppendText("PCM Seed Request Fail");
					textBox1.AppendText("\n");
					textBox1.AppendText("Wait 15 Sec Before Retry");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
					Invoke(new EventHandler(ENABLEPCMCHATTER));
					Thread.Sleep(50);
					MessageBox.Show(this, "Could Not Unlock PCM. , Wait 15 Sec Before Retrying. ", "Security Timeout ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					cancel = 1;
				}
			}
		}

		private void LISTENFORSILENCE03(object sender, EventArgs e)
		{
			if (!serialPort1.IsOpen)
			{
				return;
			}
			timeout = 0;
			while (timeout < 1000)
			{
				serialPort1.DiscardInBuffer();
				Thread.Sleep(4);
				if (serialPort1.BytesToRead > 0)
				{
					timeout++;
				}
				if (serialPort1.BytesToRead == 0)
				{
					timeout = 1000;
				}
			}
		}

		private void buttonPCMDetails_Click(object sender, EventArgs e)
		{
			cancel = 0;
			Invoke(new EventHandler(OPENPORT));
			if (cancel == 0)
			{
				Invoke(new EventHandler(SETCABLESPEED1X));
				Thread.Sleep(50);
				Invoke(new EventHandler(LISTENFORSILENCE));
				Invoke(new EventHandler(LISTENFORPROGRAMPROMPT));
				Thread.Sleep(50);
				Invoke(new EventHandler(LISTENFORSILENCE03));
				Invoke(new EventHandler(DISABLEPCMCHATTER));
				Thread.Sleep(50);
				Invoke(new EventHandler(TESTERPRESENTMESSLOSPEED));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				textBox1.AppendText("Reading PCM Details");
				textBox1.AppendText("\n");
				Invoke(new EventHandler(REQUESTHARDWARENUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTMODULE0NUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTMODULE1NUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTMODULE2NUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTMODULE3NUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTMODULE4NUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTMODULE5NUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(TESTERPRESENTMESSLOSPEED));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTMODULE6NUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTMODULE7NUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTVINNUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTSERIALNUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTBCCNUMBER));
				Thread.Sleep(50);
				Invoke(new EventHandler(REQUESTSEED));
				Thread.Sleep(50);
				Invoke(new EventHandler(ENABLEPCMCHATTER));
				Thread.Sleep(50);
				textBox1.AppendText("\n");
			}
			Invoke(new EventHandler(CLOSEPORT));
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			cancel = 1;
			if (serialPort1.IsOpen)
			{
				Invoke(new EventHandler(SETCABLESPEED1X));
				Thread.Sleep(10);
				Invoke(new EventHandler(CLOSEPORT));
			}
		}

		private void buttonReadBin_Click(object sender, EventArgs e)
		{
			cancel = 0;
			readaddress[0] = 0;
			readaddress[1] = 0;
			binoffset = 0;
			progressBar1.Value = 0;
			if (comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B")
			{
				progressBar1.Maximum = 524288;
			}
			if (comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B")
			{
				progressBar1.Maximum = 1048576;
			}
			if ((comboBoxFlashType.Text != "512KB A") & (comboBoxFlashType.Text != "512KB B") & (comboBoxFlashType.Text != "1024KB A") & (comboBoxFlashType.Text != "1024KB B"))
			{
				MessageBox.Show(this, "PCM Type Unknown \nClick PCM Details \nOr  \nSelect PCM Type ", "PCM Type Unknown", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				cancel = 1;
				return;
			}
			Invoke(new EventHandler(OPENPORT));
			if (cancel == 0)
			{
				Invoke(new EventHandler(LISTENFORSILENCE03));
				Invoke(new EventHandler(DISABLEPCMCHATTER));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(TESTERPRESENTMESSLOSPEED));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(UNLOCKPCM));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(TESTERPRESENTMESSLOSPEED));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(SWITCHPCMTOHISPEED4X));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(REQUESTMODE34A));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(TESTERPRESENTMESSAGE));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(WRITEBOOTLOADREAD));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(BOOTFROMRAM));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(TESTERPRESENTMESSAGE));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				textBox1.AppendText("Reading PCM Bin File");
				textBox1.AppendText("\n");
				Thread.Sleep(50);
			}
			if (cancel == 1)
			{
				textBox1.AppendText("Read PCM Bin Fail");
				textBox1.AppendText("\n");
				textBox1.AppendText("\n");
				Invoke(new EventHandler(CLOSEPORT));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				timerReadBin.Enabled = true;
			}
		}

		private void timerReadBin_Tick(object sender, EventArgs e)
		{
			timerReadBin.Enabled = false;
			if (cancel == 1)
			{
				binoffset = 524288;
				timerReadBin.Enabled = false;
				Invoke(new EventHandler(CLOSEPORT));
				textBox1.AppendText("Reading PCM Cancelled");
				textBox1.AppendText("\n");
				textBox1.AppendText("\n");
				return;
			}
			if (comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B")
			{
				if (binoffset < 524288)
				{
					Invoke(new EventHandler(READ800BYTES));
					Thread.Sleep(20);
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					Thread.Sleep(20);
					progressBar1.Value = binoffset;
					timerReadBin.Enabled = true;
					return;
				}
				if (binoffset >= 524288)
				{
					if (vpwmessagerx17[5] == 115)
					{
						textBox1.AppendText("Read PCM Success");
						textBox1.AppendText("\n");
						textBox1.AppendText("\n");
					}
					timerReadBin.Enabled = false;
					Invoke(new EventHandler(SENDBINTOFILE));
					Invoke(new EventHandler(CLOSEPORT));
				}
			}
			if (!(comboBoxFlashType.Text == "1024KB A") && !(comboBoxFlashType.Text == "1024KB B"))
			{
				return;
			}
			if (binoffset < 1048576)
			{
				Invoke(new EventHandler(READ800BYTES));
				Thread.Sleep(20);
				Invoke(new EventHandler(TESTERPRESENTMESSAGE));
				Thread.Sleep(20);
				progressBar1.Value = binoffset;
				timerReadBin.Enabled = true;
			}
			else if (binoffset >= 1048576)
			{
				if (vpwmessagerx17[5] == 115)
				{
					textBox1.AppendText("Read PCM Success");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				timerReadBin.Enabled = false;
				Invoke(new EventHandler(SENDBINTOFILE));
				Invoke(new EventHandler(CLOSEPORT));
			}
		}

		private void buttonWriteCal_Click(object sender, EventArgs e)
		{
			cancel = 0;
			switch (MessageBox.Show("Write Calibration File", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
			case DialogResult.No:
				cancel = 1;
				MessageBox.Show("Calibration File Write Aborted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			vpwmessagetx2062[0] = 136;
			vpwmessagetx2062[1] = 12;
			vpwmessagetx2062[2] = 109;
			vpwmessagetx2062[3] = 16;
			vpwmessagetx2062[4] = 240;
			vpwmessagetx2062[5] = 54;
			vpwmessagetx2062[6] = 0;
			vpwmessagetx2062[7] = 8;
			vpwmessagetx2062[8] = 0;
			vpwmessagetx2062[9] = byte.MaxValue;
			vpwmessagetx2062[10] = 160;
			vpwmessagetx2062[11] = 0;
			moduleNoBeingWritten = 1;
			calfileoffset = 0;
			moduleMessageSize = 2048;
			progressBar1.Value = 0;
			progressBar1.Maximum = 98304;
			Invoke(new EventHandler(OPENBINFILE));
			Invoke(new EventHandler(GETCALFILEDATA));
			Invoke(new EventHandler(GETOSFILEDATA));
			Invoke(new EventHandler(CHECKOSCALMISMATCH));
			Invoke(new EventHandler(CALCULATEOSCHECKSUM));
			Invoke(new EventHandler(OPENPORT));
			if (cancel == 0)
			{
				Invoke(new EventHandler(LISTENFORSILENCE03));
				Invoke(new EventHandler(DISABLEPCMCHATTER));
				Thread.Sleep(50);
				Invoke(new EventHandler(UNLOCKPCM));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(SWITCHPCMTOHISPEED4X));
				Thread.Sleep(50);
			}
			if (comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B")
			{
				if (cancel == 0)
				{
					Invoke(new EventHandler(REQUESTMODE34B));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE512WRITEBOOTLOADA));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE512WRITEBOOTLOADB));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(BOOTFROMRAM2));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					Thread.Sleep(50);
				}
			}
			if (comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B")
			{
				if (cancel == 0)
				{
					Invoke(new EventHandler(REQUESTMODE34C));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE1024WRITEBOOTLOADA));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE1024WRITEBOOTLOADB));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE1024WRITEBOOTLOADC));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE1024WRITEBOOTLOADD));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE1024WRITEBOOTLOADE));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(BOOTFROMRAM3));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					Thread.Sleep(50);
				}
			}
			if (cancel == 1)
			{
				textBox1.AppendText("Write Calibration Fail");
				textBox1.AppendText("\n");
				textBox1.AppendText("\n");
				timerWriteCal.Enabled = false;
			}
			if (cancel == 0)
			{
				textBox1.AppendText("Writing Calibration Modules");
				textBox1.AppendText("\n");
				Thread.Sleep(100);
				timerWriteCal.Enabled = true;
			}
		}

		private void timerWriteCal_Tick(object sender, EventArgs e)
		{
			timerWriteCal.Enabled = false;
			if (cancel == 1)
			{
				timerWriteCal.Enabled = false;
				Invoke(new EventHandler(CLOSEPORT));
				return;
			}
			if (moduleNoBeingWritten == 1)
			{
				if (modulelength1 > 2048)
				{
					moduleMessageSize = 2048;
					Invoke(new EventHandler(WRITECALMODULES));
					Thread.Sleep(20);
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					modulelength1 -= 2048;
					progressBar1.Value = calfileoffset;
					timerWriteCal.Enabled = true;
					return;
				}
				if (modulelength1 <= 2048)
				{
					vpwmessagerx7[5] = 0;
					moduleMessageSize = modulelength1;
					Invoke(new EventHandler(WRITECALMODULES));
					progressBar1.Value = calfileoffset;
					if (vpwmessagerx7[5] == 115)
					{
						textBox1.AppendText("Module 1 Write Success");
					}
					textBox1.AppendText("\n");
					moduleNoBeingWritten = 2;
					timerWriteCal.Enabled = true;
					return;
				}
			}
			if (moduleNoBeingWritten == 2)
			{
				if (modulelength2 > 2048)
				{
					moduleMessageSize = 2048;
					Invoke(new EventHandler(WRITECALMODULES));
					Thread.Sleep(20);
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					progressBar1.Value = calfileoffset;
					modulelength2 -= 2048;
					timerWriteCal.Enabled = true;
					return;
				}
				if (modulelength2 <= 2048)
				{
					vpwmessagerx7[5] = 0;
					moduleMessageSize = modulelength2;
					Invoke(new EventHandler(WRITECALMODULES));
					progressBar1.Value = calfileoffset;
					if (vpwmessagerx7[5] == 115)
					{
						textBox1.AppendText("Module 2 Write Success");
					}
					textBox1.AppendText("\n");
					moduleNoBeingWritten = 3;
					timerWriteCal.Enabled = true;
					return;
				}
			}
			if (moduleNoBeingWritten == 3)
			{
				if (modulelength3 > 2048)
				{
					moduleMessageSize = 2048;
					Invoke(new EventHandler(WRITECALMODULES));
					Thread.Sleep(20);
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					progressBar1.Value = calfileoffset;
					modulelength3 -= 2048;
					timerWriteCal.Enabled = true;
					return;
				}
				if (modulelength3 <= 2048)
				{
					vpwmessagerx7[5] = 0;
					moduleMessageSize = modulelength3;
					Invoke(new EventHandler(WRITECALMODULES));
					progressBar1.Value = calfileoffset;
					if (vpwmessagerx7[5] == 115)
					{
						textBox1.AppendText("Module 3 Write Success");
					}
					textBox1.AppendText("\n");
					moduleNoBeingWritten = 4;
					timerWriteCal.Enabled = true;
					return;
				}
			}
			if (moduleNoBeingWritten == 4)
			{
				if (modulelength4 > 2048)
				{
					moduleMessageSize = 2048;
					Invoke(new EventHandler(WRITECALMODULES));
					Thread.Sleep(20);
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					progressBar1.Value = calfileoffset;
					modulelength4 -= 2048;
					timerWriteCal.Enabled = true;
					return;
				}
				if (modulelength4 <= 2048)
				{
					vpwmessagerx7[5] = 0;
					moduleMessageSize = modulelength4;
					Invoke(new EventHandler(WRITECALMODULES));
					progressBar1.Value = calfileoffset;
					if (vpwmessagerx7[5] == 115)
					{
						textBox1.AppendText("Module 4 Write Success");
					}
					textBox1.AppendText("\n");
					moduleNoBeingWritten = 5;
					timerWriteCal.Enabled = true;
					return;
				}
			}
			if (moduleNoBeingWritten == 5)
			{
				if (modulelength5 > 2048)
				{
					moduleMessageSize = 2048;
					Invoke(new EventHandler(WRITECALMODULES));
					Thread.Sleep(20);
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					progressBar1.Value = calfileoffset;
					modulelength5 -= 2048;
					timerWriteCal.Enabled = true;
					return;
				}
				if (modulelength5 <= 2048)
				{
					vpwmessagerx7[5] = 0;
					moduleMessageSize = modulelength5;
					Invoke(new EventHandler(WRITECALMODULES));
					progressBar1.Value = calfileoffset;
					if (vpwmessagerx7[5] == 115)
					{
						textBox1.AppendText("Module 5 Write Success");
					}
					textBox1.AppendText("\n");
					moduleNoBeingWritten = 6;
					timerWriteCal.Enabled = true;
					return;
				}
			}
			if (moduleNoBeingWritten == 6)
			{
				if (modulelength6 > 2048)
				{
					moduleMessageSize = 2048;
					Invoke(new EventHandler(WRITECALMODULES));
					Thread.Sleep(20);
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					progressBar1.Value = calfileoffset;
					modulelength6 -= 2048;
					timerWriteCal.Enabled = true;
					return;
				}
				if (modulelength6 <= 2048)
				{
					vpwmessagerx7[5] = 0;
					moduleMessageSize = modulelength6;
					Invoke(new EventHandler(WRITECALMODULES));
					progressBar1.Value = calfileoffset;
					if (vpwmessagerx7[5] == 115)
					{
						textBox1.AppendText("Module 6 Write Success");
					}
					textBox1.AppendText("\n");
					moduleNoBeingWritten = 7;
					timerWriteCal.Enabled = true;
					return;
				}
			}
			if (moduleNoBeingWritten == 7)
			{
				if (modulelength7 > 2048)
				{
					moduleMessageSize = 2048;
					Invoke(new EventHandler(WRITECALMODULES));
					Thread.Sleep(20);
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					progressBar1.Value = calfileoffset;
					modulelength7 -= 2048;
					timerWriteCal.Enabled = true;
					return;
				}
				if (modulelength7 <= 2048)
				{
					vpwmessagerx7[5] = 0;
					moduleMessageSize = modulelength7;
					Invoke(new EventHandler(WRITECALMODULES));
					progressBar1.Value = calfileoffset;
					moduleNoBeingWritten = 8;
					timerWriteCal.Enabled = true;
					return;
				}
			}
			if (moduleNoBeingWritten == 8)
			{
				timerWriteCal.Enabled = false;
				textBox1.AppendText("\n");
				textBox1.AppendText("Bin File Written");
				textBox1.AppendText("\n");
				textBox1.AppendText(BinFileName);
				textBox1.AppendText("\n");
				textBox1.AppendText("\n");
				Invoke(new EventHandler(CLOSEPORT));
			}
		}

		private void buttonWriteBin_Click(object sender, EventArgs e)
		{
			cancel = 0;
			switch (MessageBox.Show("Write Bin File ! \n\nOnly Use Write Bin To Change Operating System \nUse Write Cal To Change Calibration Settings", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
			case DialogResult.No:
				cancel = 1;
				MessageBox.Show("Bin File Write Aborted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			vpwmessagetx2062[0] = 136;
			vpwmessagetx2062[1] = 12;
			vpwmessagetx2062[2] = 109;
			vpwmessagetx2062[3] = 16;
			vpwmessagetx2062[4] = 240;
			vpwmessagetx2062[5] = 54;
			vpwmessagetx2062[6] = 0;
			vpwmessagetx2062[7] = 8;
			vpwmessagetx2062[8] = 0;
			vpwmessagetx2062[9] = byte.MaxValue;
			vpwmessagetx2062[10] = 160;
			vpwmessagetx2062[11] = 0;
			osfileoffset = 0;
			moduleNoBeingWritten = 1;
			calfileoffset = 0;
			moduleMessageSize = 2048;
			Invoke(new EventHandler(OPENBINFILE));
			if (cancel == 0)
			{
				Invoke(new EventHandler(GETCALFILEDATA));
				Invoke(new EventHandler(GETOSFILEDATA));
				Invoke(new EventHandler(CALCULATEOSCHECKSUM));
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(OPENPORT));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(LISTENFORSILENCE03));
				Invoke(new EventHandler(DISABLEPCMCHATTER));
				Thread.Sleep(50);
				Invoke(new EventHandler(TESTERPRESENTMESSLOSPEED));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(UNLOCKPCM));
				Thread.Sleep(50);
				Invoke(new EventHandler(TESTERPRESENTMESSLOSPEED));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(SWITCHPCMTOHISPEED4X));
				Thread.Sleep(50);
			}
			if (comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B")
			{
				if (cancel == 0)
				{
					progressBar1.Value = 0;
					progressBar1.Maximum = 409600;
					Invoke(new EventHandler(REQUESTMODE34B));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE512WRITEBOOTLOADA));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE512WRITEBOOTLOADB));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(BOOTFROMRAM2));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					Thread.Sleep(50);
				}
			}
			if (comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B")
			{
				if (cancel == 0)
				{
					progressBar1.Value = 0;
					progressBar1.Maximum = 933888;
					Invoke(new EventHandler(REQUESTMODE34C));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE1024WRITEBOOTLOADA));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE1024WRITEBOOTLOADB));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE1024WRITEBOOTLOADC));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE1024WRITEBOOTLOADD));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(WRITE1024WRITEBOOTLOADE));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(BOOTFROMRAM3));
					Thread.Sleep(50);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					Thread.Sleep(50);
				}
			}
			if (cancel == 1)
			{
				textBox1.AppendText("Write Operating System Fail");
				textBox1.AppendText("\n");
				textBox1.AppendText("\n");
				timerWriteBin.Enabled = false;
			}
			if (cancel == 0)
			{
				textBox1.AppendText("Writing Operating System");
				textBox1.AppendText("\n");
				Thread.Sleep(50);
				timerWriteBin.Enabled = true;
			}
		}

		private void timerWriteBin_Tick(object sender, EventArgs e)
		{
			timerWriteBin.Enabled = false;
			if (cancel == 1)
			{
				timerWriteBin.Enabled = false;
				Invoke(new EventHandler(CLOSEPORT));
				return;
			}
			if (comboBoxFlashType.Text == "512KB A" || comboBoxFlashType.Text == "512KB B")
			{
				if (osfileoffset < 409600)
				{
					Invoke(new EventHandler(WRITEOSBLOCK0X800));
					Thread.Sleep(20);
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					progressBar1.Value = osfileoffset;
					timerWriteBin.Enabled = true;
					return;
				}
				if (osfileoffset >= 409600)
				{
					timerWriteBin.Enabled = false;
					progressBar1.Maximum = 98304;
					textBox1.AppendText("Writing Calibration Modules");
					textBox1.AppendText("\n");
					timerWriteCal.Enabled = true;
					return;
				}
			}
			if (comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B")
			{
				if (osfileoffset < 933888)
				{
					Invoke(new EventHandler(WRITEOSBLOCK0X800));
					Thread.Sleep(20);
					Invoke(new EventHandler(TESTERPRESENTMESSAGE));
					progressBar1.Value = osfileoffset;
					timerWriteBin.Enabled = true;
				}
				else if (osfileoffset >= 933888)
				{
					timerWriteBin.Enabled = false;
					progressBar1.Maximum = 98304;
					textBox1.AppendText("Writing Calibration Modules");
					textBox1.AppendText("\n");
					timerWriteCal.Enabled = true;
				}
			}
		}

		private void buttonWriteSec_Click(object sender, EventArgs e)
		{
			cancel = 0;
			Invoke(new EventHandler(OPENBINFILE));
			Invoke(new EventHandler(GETSECURITYFILEDATA));
			Invoke(new EventHandler(OPENPORT));
			if (!serialPort1.IsOpen)
			{
				return;
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(LISTENFORSILENCE03));
				Invoke(new EventHandler(DISABLEPCMCHATTER));
				Thread.Sleep(50);
				if (cancel == 0)
				{
					Invoke(new EventHandler(UNLOCKPCM));
					Thread.Sleep(10);
					Invoke(new EventHandler(REQUESTMODE34D));
					Thread.Sleep(10);
				}
				if (cancel == 0)
				{
					textBox1.AppendText("Writng PCM Security Block");
					textBox1.AppendText("\n");
					Invoke(new EventHandler(WRITESECURITYBLOCK0X50));
					Thread.Sleep(10);
				}
				textBox1.AppendText("\n");
			}
			Invoke(new EventHandler(CLOSEPORT));
		}

		private void buttonWriteKey_Click(object sender, EventArgs e)
		{
			cancel = 0;
			Invoke(new EventHandler(OPENPORT));
			if (!serialPort1.IsOpen)
			{
				return;
			}
			Invoke(new EventHandler(LISTENFORSILENCE03));
			Invoke(new EventHandler(DISABLEPCMCHATTER));
			Thread.Sleep(50);
			if (cancel == 0)
			{
				Invoke(new EventHandler(UNLOCKPCM));
				Thread.Sleep(10);
			}
			if (cancel == 0)
			{
				Invoke(new EventHandler(REQUESTMODE34E));
				Thread.Sleep(10);
			}
			if (cancel == 0)
			{
				textBox1.AppendText("Writng PCM Seed and Key");
				textBox1.AppendText("\n");
				if (comboBoxFlashType.Text == "512KB A")
				{
					serialPort1.Write(new byte[18]
					{
						0,
						16,
						108,
						16,
						240,
						54,
						0,
						0,
						4,
						255,
						131,
						112,
						74,
						60,
						87,
						3,
						2,
						214
					}, 0, 18);
					Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCEB));
				}
				if (comboBoxFlashType.Text == "1024KB A" || comboBoxFlashType.Text == "1024KB B" || comboBoxFlashType.Text == "512KB B")
				{
					serialPort1.Write(new byte[18]
					{
						0,
						16,
						108,
						16,
						240,
						54,
						0,
						0,
						4,
						255,
						128,
						0,
						74,
						60,
						87,
						3,
						2,
						99
					}, 0, 18);
					Invoke(new EventHandler(LISTENFORVPW7BYTERESPONCEB));
				}
				textBox1.AppendText("\n");
			}
			Invoke(new EventHandler(CLOSEPORT));
		}

		private void buttonWriteVin_Click(object sender, EventArgs e)
		{
			cancel = 0;
			if (textBoxVIN.TextLength != 17)
			{
				MessageBox.Show(this, "Incorrect VIN Data", "Incorrect VIN Data", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				cancel = 1;
				return;
			}
			Invoke(new EventHandler(OPENPORT));
			buffer0 = textBoxVIN.Text;
			vinbuffer[0] = 0;
			vinbuffer[1] = Convert.ToByte(buffer0[0]);
			vinbuffer[2] = Convert.ToByte(buffer0[1]);
			vinbuffer[3] = Convert.ToByte(buffer0[2]);
			vinbuffer[4] = Convert.ToByte(buffer0[3]);
			vinbuffer[5] = Convert.ToByte(buffer0[4]);
			vinbuffer[6] = Convert.ToByte(buffer0[5]);
			vinbuffer[7] = Convert.ToByte(buffer0[6]);
			vinbuffer[8] = Convert.ToByte(buffer0[7]);
			vinbuffer[9] = Convert.ToByte(buffer0[8]);
			vinbuffer[10] = Convert.ToByte(buffer0[9]);
			vinbuffer[11] = Convert.ToByte(buffer0[10]);
			vinbuffer[12] = Convert.ToByte(buffer0[11]);
			vinbuffer[13] = Convert.ToByte(buffer0[12]);
			vinbuffer[14] = Convert.ToByte(buffer0[13]);
			vinbuffer[15] = Convert.ToByte(buffer0[14]);
			vinbuffer[16] = Convert.ToByte(buffer0[15]);
			vinbuffer[17] = Convert.ToByte(buffer0[16]);
			if (!serialPort1.IsOpen)
			{
				return;
			}
			vpwmessagerx5[3] = 0;
			if (cancel == 0)
			{
				Invoke(new EventHandler(LISTENFORSILENCE03));
				Invoke(new EventHandler(DISABLEPCMCHATTER));
				Thread.Sleep(50);
			}
			if (cancel == 0)
			{
				textBox1.AppendText("Writng PCM VIN Number A");
				textBox1.AppendText("\n");
				Invoke(new EventHandler(LISTENFORSILENCE03));
				serialPort1.DiscardInBuffer();
				SerialPort serialPort = serialPort1;
				byte[] array = new byte[13]
				{
					0,
					11,
					108,
					16,
					240,
					59,
					1,
					0,
					0,
					0,
					0,
					0,
					0
				};
				array[7] = vinbuffer[0];
				array[8] = vinbuffer[1];
				array[9] = vinbuffer[2];
				array[10] = vinbuffer[3];
				array[11] = vinbuffer[4];
				array[12] = vinbuffer[5];
				serialPort.Write(array, 0, 13);
				Invoke(new EventHandler(LISTENFORVPW5BYTERESPONCE));
				if (vpwmessagerx5[3] != 123)
				{
					textBox1.AppendText("VIN Write Fail");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
					cancel = 1;
				}
				if (vpwmessagerx5[3] == 123)
				{
					textBox1.AppendText("VIN Write Success");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				Thread.Sleep(50);
				textBox1.AppendText("Writng PCM VIN Number B");
				textBox1.AppendText("\n");
				Invoke(new EventHandler(LISTENFORSILENCE03));
				serialPort1.DiscardInBuffer();
				SerialPort serialPort2 = serialPort1;
				array = new byte[13]
				{
					0,
					11,
					108,
					16,
					240,
					59,
					2,
					0,
					0,
					0,
					0,
					0,
					0
				};
				array[7] = vinbuffer[6];
				array[8] = vinbuffer[7];
				array[9] = vinbuffer[8];
				array[10] = vinbuffer[9];
				array[11] = vinbuffer[10];
				array[12] = vinbuffer[11];
				serialPort2.Write(array, 0, 13);
				Invoke(new EventHandler(LISTENFORVPW5BYTERESPONCE));
				if (vpwmessagerx5[3] != 123)
				{
					textBox1.AppendText("VIN Write Fail");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
					cancel = 1;
				}
				if (vpwmessagerx5[3] == 123)
				{
					textBox1.AppendText("VIN Write Success");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				Thread.Sleep(50);
				textBox1.AppendText("Writng PCM VIN Number C");
				textBox1.AppendText("\n");
				Invoke(new EventHandler(LISTENFORSILENCE03));
				serialPort1.DiscardInBuffer();
				SerialPort serialPort3 = serialPort1;
				array = new byte[13]
				{
					0,
					11,
					108,
					16,
					240,
					59,
					3,
					0,
					0,
					0,
					0,
					0,
					0
				};
				array[7] = vinbuffer[12];
				array[8] = vinbuffer[13];
				array[9] = vinbuffer[14];
				array[10] = vinbuffer[15];
				array[11] = vinbuffer[16];
				array[12] = vinbuffer[17];
				serialPort3.Write(array, 0, 13);
				Invoke(new EventHandler(LISTENFORVPW5BYTERESPONCE));
				Thread.Sleep(50);
				if (vpwmessagerx5[3] != 123)
				{
					textBox1.AppendText("VIN Write Fail");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
					cancel = 1;
				}
				if (vpwmessagerx5[3] == 123)
				{
					textBox1.AppendText("VIN Write Success");
					textBox1.AppendText("\n");
					textBox1.AppendText("\n");
				}
				Invoke(new EventHandler(ENABLEPCMCHATTER));
				textBox1.AppendText("\n");
			}
			Invoke(new EventHandler(CLOSEPORT));
		}

		private void buttonWriteSerial_Click(object sender, EventArgs e)
		{
			cancel = 0;
			if (textBoxSerialNumber.TextLength != 12)
			{
				MessageBox.Show(this, "Incorrect Serial Number Data", "Incorrect Serial Number Data", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				cancel = 1;
				return;
			}
			Invoke(new EventHandler(GETSERIALNUMBERDATA));
			Invoke(new EventHandler(OPENPORT));
			if (serialPort1.IsOpen)
			{
				if (cancel == 0)
				{
					Invoke(new EventHandler(LISTENFORSILENCE03));
					Invoke(new EventHandler(DISABLEPCMCHATTER));
					Thread.Sleep(10);
					Invoke(new EventHandler(UNLOCKPCM));
					Thread.Sleep(10);
				}
				if (cancel == 0)
				{
					Invoke(new EventHandler(REQUESTMODE34D));
					Thread.Sleep(10);
				}
				if (cancel == 0)
				{
					textBox1.AppendText("Writng PCM Serial Number");
					textBox1.AppendText("\n");
					Invoke(new EventHandler(WRITESERIALNUMBERBLOCK0X1A));
					Thread.Sleep(10);
					textBox1.AppendText("\n");
				}
				Invoke(new EventHandler(CLOSEPORT));
			}
		}

		private void timerDisableChatter_Tick(object sender, EventArgs e)
		{
			timerDisableChatter.Enabled = false;
			if (cancel == 0 && serialPort1.IsOpen)
			{
				Invoke(new EventHandler(LISTENFORSILENCE));
				serialPort1.DiscardInBuffer();
				serialPort1.Write(new byte[7]
				{
					0,
					5,
					108,
					16,
					240,
					40,
					0
				}, 0, 7);
				Invoke(new EventHandler(LISTENFORVPW6BYTERESPONCE));
				if (vpwmessagerx6[3] != 104)
				{
					textBox1.AppendText("DISABLE CHATTER FAILURE");
					textBox1.AppendText("\n");
					timerDisableChatter.Enabled = true;
				}
			}
		}

		private void buttonReadDTC_Click(object sender, EventArgs e)
		{
			cancel = 0;
			Invoke(new EventHandler(OPENPORT));
			if (serialPort1.IsOpen && cancel == 0)
			{
				Invoke(new EventHandler(SETCABLESPEED1X));
				Thread.Sleep(100);
				Invoke(new EventHandler(LISTENFORSILENCE03));
				Invoke(new EventHandler(DISABLEPCMCHATTER));
				Thread.Sleep(50);
				Invoke(new EventHandler(TESTERPRESENTMESSLOSPEED));
				Thread.Sleep(50);
				Invoke(new EventHandler(READALLPCMCODES));
			}
			Invoke(new EventHandler(CLOSEPORT));
		}

		private void buttonClearDTC_Click(object sender, EventArgs e)
		{
			cancel = 0;
			Invoke(new EventHandler(OPENPORT));
			if (serialPort1.IsOpen && cancel == 0)
			{
				Invoke(new EventHandler(SETCABLESPEED1X));
				Thread.Sleep(100);
				Invoke(new EventHandler(LISTENFORSILENCE03));
				Invoke(new EventHandler(DISABLEPCMCHATTER));
				Thread.Sleep(50);
				Invoke(new EventHandler(TESTERPRESENTMESSLOSPEED));
				Thread.Sleep(50);
				Invoke(new EventHandler(CLEARPCMCODES));
			}
			Invoke(new EventHandler(CLOSEPORT));
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			comboBoxComPort = new System.Windows.Forms.ComboBox();
			textBox1 = new System.Windows.Forms.TextBox();
			buttonPCMDetails = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			textBoxSeed = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxKey = new System.Windows.Forms.TextBox();
			serialPort1 = new System.IO.Ports.SerialPort(components);
			textBoxModule0 = new System.Windows.Forms.TextBox();
			textBoxModule1 = new System.Windows.Forms.TextBox();
			textBoxModule2 = new System.Windows.Forms.TextBox();
			textBoxModule3 = new System.Windows.Forms.TextBox();
			textBoxModule4 = new System.Windows.Forms.TextBox();
			textBoxModule5 = new System.Windows.Forms.TextBox();
			textBoxModule6 = new System.Windows.Forms.TextBox();
			textBoxModule7 = new System.Windows.Forms.TextBox();
			textBoxVIN = new System.Windows.Forms.TextBox();
			textBoxSerialNumber = new System.Windows.Forms.TextBox();
			textBoxBCC = new System.Windows.Forms.TextBox();
			textBoxPCMHardwareNo = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			buttonCancel = new System.Windows.Forms.Button();
			buttonReadBin = new System.Windows.Forms.Button();
			timerReadBin = new System.Windows.Forms.Timer(components);
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			progressBar1 = new System.Windows.Forms.ProgressBar();
			comboBoxFlashType = new System.Windows.Forms.ComboBox();
			buttonWriteCal = new System.Windows.Forms.Button();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			timerWriteCal = new System.Windows.Forms.Timer(components);
			buttonWriteBin = new System.Windows.Forms.Button();
			timerWriteBin = new System.Windows.Forms.Timer(components);
			buttonWriteSec = new System.Windows.Forms.Button();
			buttonWriteVin = new System.Windows.Forms.Button();
			buttonWriteSerial = new System.Windows.Forms.Button();
			buttonWriteKey = new System.Windows.Forms.Button();
			timerDisableChatter = new System.Windows.Forms.Timer(components);
			label17 = new System.Windows.Forms.Label();
			buttonReadDTC = new System.Windows.Forms.Button();
			buttonClearDTC = new System.Windows.Forms.Button();
			label16 = new System.Windows.Forms.Label();
			SuspendLayout();
			comboBoxComPort.FormattingEnabled = true;
			comboBoxComPort.Location = new System.Drawing.Point(93, 28);
			comboBoxComPort.Name = "comboBoxComPort";
			comboBoxComPort.Size = new System.Drawing.Size(65, 21);
			comboBoxComPort.TabIndex = 1;
			comboBoxComPort.Text = "COM1";
			comboBoxComPort.DropDown += new System.EventHandler(comboBoxComPort_DropDown);
			textBox1.Location = new System.Drawing.Point(318, 12);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBox1.Size = new System.Drawing.Size(167, 309);
			textBox1.TabIndex = 2;
			buttonPCMDetails.Location = new System.Drawing.Point(12, 12);
			buttonPCMDetails.Name = "buttonPCMDetails";
			buttonPCMDetails.Size = new System.Drawing.Size(75, 23);
			buttonPCMDetails.TabIndex = 3;
			buttonPCMDetails.Text = "PCM Details";
			buttonPCMDetails.UseVisualStyleBackColor = true;
			buttonPCMDetails.Click += new System.EventHandler(buttonPCMDetails_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(93, 91);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(32, 13);
			label1.TabIndex = 4;
			label1.Text = "Seed";
			textBoxSeed.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSeed.Location = new System.Drawing.Point(93, 107);
			textBoxSeed.Name = "textBoxSeed";
			textBoxSeed.Size = new System.Drawing.Size(42, 20);
			textBoxSeed.TabIndex = 5;
			textBoxSeed.Text = "0000";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(93, 130);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(25, 13);
			label2.TabIndex = 6;
			label2.Text = "Key";
			textBoxKey.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxKey.Location = new System.Drawing.Point(93, 146);
			textBoxKey.Name = "textBoxKey";
			textBoxKey.Size = new System.Drawing.Size(42, 20);
			textBoxKey.TabIndex = 7;
			textBoxKey.Text = "0000";
			serialPort1.BaudRate = 115200;
			serialPort1.ReadTimeout = 1;
			serialPort1.WriteBufferSize = 4096;
			textBoxModule0.BackColor = System.Drawing.SystemColors.InactiveCaption;
			textBoxModule0.Location = new System.Drawing.Point(222, 28);
			textBoxModule0.Name = "textBoxModule0";
			textBoxModule0.Size = new System.Drawing.Size(68, 20);
			textBoxModule0.TabIndex = 8;
			textBoxModule1.Location = new System.Drawing.Point(222, 67);
			textBoxModule1.Name = "textBoxModule1";
			textBoxModule1.Size = new System.Drawing.Size(68, 20);
			textBoxModule1.TabIndex = 9;
			textBoxModule2.Location = new System.Drawing.Point(222, 106);
			textBoxModule2.Name = "textBoxModule2";
			textBoxModule2.Size = new System.Drawing.Size(68, 20);
			textBoxModule2.TabIndex = 10;
			textBoxModule3.Location = new System.Drawing.Point(222, 145);
			textBoxModule3.Name = "textBoxModule3";
			textBoxModule3.Size = new System.Drawing.Size(68, 20);
			textBoxModule3.TabIndex = 11;
			textBoxModule4.Location = new System.Drawing.Point(222, 184);
			textBoxModule4.Name = "textBoxModule4";
			textBoxModule4.Size = new System.Drawing.Size(68, 20);
			textBoxModule4.TabIndex = 12;
			textBoxModule5.Location = new System.Drawing.Point(222, 223);
			textBoxModule5.Name = "textBoxModule5";
			textBoxModule5.Size = new System.Drawing.Size(68, 20);
			textBoxModule5.TabIndex = 13;
			textBoxModule6.Location = new System.Drawing.Point(222, 262);
			textBoxModule6.Name = "textBoxModule6";
			textBoxModule6.Size = new System.Drawing.Size(68, 20);
			textBoxModule6.TabIndex = 14;
			textBoxModule7.Location = new System.Drawing.Point(222, 301);
			textBoxModule7.Name = "textBoxModule7";
			textBoxModule7.Size = new System.Drawing.Size(68, 20);
			textBoxModule7.TabIndex = 15;
			textBoxVIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxVIN.Location = new System.Drawing.Point(96, 301);
			textBoxVIN.Name = "textBoxVIN";
			textBoxVIN.Size = new System.Drawing.Size(123, 20);
			textBoxVIN.TabIndex = 16;
			textBoxSerialNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSerialNumber.Location = new System.Drawing.Point(93, 264);
			textBoxSerialNumber.Name = "textBoxSerialNumber";
			textBoxSerialNumber.Size = new System.Drawing.Size(93, 20);
			textBoxSerialNumber.TabIndex = 17;
			textBoxBCC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxBCC.Location = new System.Drawing.Point(93, 68);
			textBoxBCC.Name = "textBoxBCC";
			textBoxBCC.Size = new System.Drawing.Size(42, 20);
			textBoxBCC.TabIndex = 18;
			textBoxPCMHardwareNo.Location = new System.Drawing.Point(93, 225);
			textBoxPCMHardwareNo.Name = "textBoxPCMHardwareNo";
			textBoxPCMHardwareNo.Size = new System.Drawing.Size(75, 20);
			textBoxPCMHardwareNo.TabIndex = 19;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(222, 12);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(90, 13);
			label3.TabIndex = 21;
			label3.Text = "Operating System";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(222, 51);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(40, 13);
			label4.TabIndex = 22;
			label4.Text = "Engine";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(222, 90);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(93, 13);
			label5.TabIndex = 23;
			label5.Text = "Engine Diagnostic";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(223, 129);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(68, 13);
			label6.TabIndex = 24;
			label6.Text = "Transmission";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(223, 168);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(87, 13);
			label7.TabIndex = 25;
			label7.Text = "Trans Diagnostic";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(222, 207);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(64, 13);
			label8.TabIndex = 26;
			label8.Text = "Fuel System";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(222, 246);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(41, 13);
			label9.TabIndex = 27;
			label9.Text = "System";
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(222, 285);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(44, 13);
			label10.TabIndex = 28;
			label10.Text = "Speedo";
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(93, 287);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(25, 13);
			label11.TabIndex = 29;
			label11.Text = "VIN";
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(93, 248);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(73, 13);
			label12.TabIndex = 30;
			label12.Text = "Serial Number";
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(93, 52);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(28, 13);
			label13.TabIndex = 31;
			label13.Text = "BCC";
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(93, 169);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(57, 13);
			label14.TabIndex = 32;
			label14.Text = "PCM Type";
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(93, 209);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(93, 13);
			label15.TabIndex = 33;
			label15.Text = "Hardware Number";
			buttonCancel.Location = new System.Drawing.Point(12, 302);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(75, 23);
			buttonCancel.TabIndex = 34;
			buttonCancel.Text = "Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonReadBin.Location = new System.Drawing.Point(12, 41);
			buttonReadBin.Name = "buttonReadBin";
			buttonReadBin.Size = new System.Drawing.Size(75, 23);
			buttonReadBin.TabIndex = 35;
			buttonReadBin.Text = "Read Bin";
			buttonReadBin.UseVisualStyleBackColor = true;
			buttonReadBin.Click += new System.EventHandler(buttonReadBin_Click);
			timerReadBin.Interval = 40;
			timerReadBin.Tick += new System.EventHandler(timerReadBin_Tick);
			saveFileDialog1.Title = "Save Bin File";
			progressBar1.Location = new System.Drawing.Point(241, 327);
			progressBar1.Maximum = 524288;
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new System.Drawing.Size(244, 13);
			progressBar1.TabIndex = 36;
			comboBoxFlashType.FormattingEnabled = true;
			comboBoxFlashType.Items.AddRange(new object[4]
			{
				"512KB A",
				"512KB B",
				"1024KB A",
				"1024KB B"
			});
			comboBoxFlashType.Location = new System.Drawing.Point(93, 185);
			comboBoxFlashType.Name = "comboBoxFlashType";
			comboBoxFlashType.Size = new System.Drawing.Size(72, 21);
			comboBoxFlashType.TabIndex = 37;
			buttonWriteCal.BackColor = System.Drawing.SystemColors.ButtonFace;
			buttonWriteCal.Location = new System.Drawing.Point(12, 70);
			buttonWriteCal.Name = "buttonWriteCal";
			buttonWriteCal.Size = new System.Drawing.Size(75, 23);
			buttonWriteCal.TabIndex = 38;
			buttonWriteCal.Text = "Write Cal";
			buttonWriteCal.UseVisualStyleBackColor = false;
			buttonWriteCal.Click += new System.EventHandler(buttonWriteCal_Click);
			openFileDialog1.FileName = "Select Bin File";
			openFileDialog1.RestoreDirectory = true;
			openFileDialog1.Title = "Open Bin File";
			timerWriteCal.Interval = 40;
			timerWriteCal.Tick += new System.EventHandler(timerWriteCal_Tick);
			buttonWriteBin.BackColor = System.Drawing.SystemColors.ButtonFace;
			buttonWriteBin.Location = new System.Drawing.Point(12, 215);
			buttonWriteBin.Name = "buttonWriteBin";
			buttonWriteBin.Size = new System.Drawing.Size(75, 23);
			buttonWriteBin.TabIndex = 39;
			buttonWriteBin.Text = "Write Bin";
			buttonWriteBin.UseVisualStyleBackColor = false;
			buttonWriteBin.Click += new System.EventHandler(buttonWriteBin_Click);
			timerWriteBin.Interval = 40;
			timerWriteBin.Tick += new System.EventHandler(timerWriteBin_Tick);
			buttonWriteSec.Location = new System.Drawing.Point(12, 186);
			buttonWriteSec.Name = "buttonWriteSec";
			buttonWriteSec.Size = new System.Drawing.Size(75, 23);
			buttonWriteSec.TabIndex = 40;
			buttonWriteSec.Text = "Write Sec";
			buttonWriteSec.UseVisualStyleBackColor = true;
			buttonWriteSec.Click += new System.EventHandler(buttonWriteSec_Click);
			buttonWriteVin.Location = new System.Drawing.Point(12, 99);
			buttonWriteVin.Name = "buttonWriteVin";
			buttonWriteVin.Size = new System.Drawing.Size(75, 23);
			buttonWriteVin.TabIndex = 41;
			buttonWriteVin.Text = "Write VIN";
			buttonWriteVin.UseVisualStyleBackColor = true;
			buttonWriteVin.Click += new System.EventHandler(buttonWriteVin_Click);
			buttonWriteSerial.Location = new System.Drawing.Point(12, 128);
			buttonWriteSerial.Name = "buttonWriteSerial";
			buttonWriteSerial.Size = new System.Drawing.Size(75, 23);
			buttonWriteSerial.TabIndex = 42;
			buttonWriteSerial.Text = "Write S/N";
			buttonWriteSerial.UseVisualStyleBackColor = true;
			buttonWriteSerial.Click += new System.EventHandler(buttonWriteSerial_Click);
			buttonWriteKey.Location = new System.Drawing.Point(12, 157);
			buttonWriteKey.Name = "buttonWriteKey";
			buttonWriteKey.Size = new System.Drawing.Size(75, 23);
			buttonWriteKey.TabIndex = 43;
			buttonWriteKey.Text = "Write Key";
			buttonWriteKey.UseVisualStyleBackColor = true;
			buttonWriteKey.Click += new System.EventHandler(buttonWriteKey_Click);
			timerDisableChatter.Interval = 15000;
			timerDisableChatter.Tick += new System.EventHandler(timerDisableChatter_Tick);
			label17.AutoSize = true;
			label17.ForeColor = System.Drawing.SystemColors.HotTrack;
			label17.Location = new System.Drawing.Point(9, 328);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(226, 13);
			label17.TabIndex = 46;
			label17.Text = "All Rights Reserved CommodoreHackers 2018";
			buttonReadDTC.Location = new System.Drawing.Point(12, 244);
			buttonReadDTC.Name = "buttonReadDTC";
			buttonReadDTC.Size = new System.Drawing.Size(75, 23);
			buttonReadDTC.TabIndex = 47;
			buttonReadDTC.Text = "Read DTC";
			buttonReadDTC.UseVisualStyleBackColor = true;
			buttonReadDTC.Click += new System.EventHandler(buttonReadDTC_Click);
			buttonClearDTC.Location = new System.Drawing.Point(12, 273);
			buttonClearDTC.Name = "buttonClearDTC";
			buttonClearDTC.Size = new System.Drawing.Size(75, 23);
			buttonClearDTC.TabIndex = 48;
			buttonClearDTC.Text = "Clear DTC";
			buttonClearDTC.UseVisualStyleBackColor = true;
			buttonClearDTC.Click += new System.EventHandler(buttonClearDTC_Click);
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(93, 12);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(50, 13);
			label16.TabIndex = 49;
			label16.Text = "Com Port";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(497, 349);
			base.Controls.Add(label16);
			base.Controls.Add(buttonClearDTC);
			base.Controls.Add(buttonReadDTC);
			base.Controls.Add(label17);
			base.Controls.Add(buttonWriteKey);
			base.Controls.Add(buttonWriteSerial);
			base.Controls.Add(buttonWriteVin);
			base.Controls.Add(buttonWriteSec);
			base.Controls.Add(buttonWriteBin);
			base.Controls.Add(buttonWriteCal);
			base.Controls.Add(comboBoxFlashType);
			base.Controls.Add(progressBar1);
			base.Controls.Add(buttonReadBin);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(label15);
			base.Controls.Add(label14);
			base.Controls.Add(label13);
			base.Controls.Add(label12);
			base.Controls.Add(label11);
			base.Controls.Add(label10);
			base.Controls.Add(label9);
			base.Controls.Add(label8);
			base.Controls.Add(label7);
			base.Controls.Add(label6);
			base.Controls.Add(label5);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxPCMHardwareNo);
			base.Controls.Add(textBoxBCC);
			base.Controls.Add(textBoxSerialNumber);
			base.Controls.Add(textBoxVIN);
			base.Controls.Add(textBoxModule7);
			base.Controls.Add(textBoxModule6);
			base.Controls.Add(textBoxModule5);
			base.Controls.Add(textBoxModule4);
			base.Controls.Add(textBoxModule3);
			base.Controls.Add(textBoxModule2);
			base.Controls.Add(textBoxModule1);
			base.Controls.Add(textBoxModule0);
			base.Controls.Add(textBoxKey);
			base.Controls.Add(label2);
			base.Controls.Add(textBoxSeed);
			base.Controls.Add(label1);
			base.Controls.Add(buttonPCMDetails);
			base.Controls.Add(textBox1);
			base.Controls.Add(comboBoxComPort);
			base.Name = "Form1";
			Text = "LS1 FLASH TOOL V2.06";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
