import { red, orange, green, blue } from '@ant-design/colors';
import Icon, {
  FileExcelOutlined,
  FileGifOutlined,
  FileImageOutlined,
  FileJpgOutlined,
  FileOutlined,
  FilePdfOutlined,
  FilePptOutlined,
  FileTextOutlined,
  FileWordOutlined,
  FileZipOutlined,
} from '@ant-design/icons';
import { GetProps } from 'antd';

type IconProps = Omit<GetProps<typeof Icon>, 'component'>;

interface Props extends IconProps {
  extension?: string;
}

export function CourseMaterialFileIcon({ extension, ...props }: Props) {
  const fileExtension = extension?.split('.').pop()?.toLowerCase();
  const iconProps = { title: fileExtension, ...props };

  switch (fileExtension) {
    case 'pdf':
      return <FilePdfOutlined style={{ color: red.primary }} {...iconProps} />;
    case 'ppt':
    case 'pptx':
      return (
        <FilePptOutlined style={{ color: orange.primary }} {...iconProps} />
      );
    case 'doc':
    case 'docx':
      return (
        <FileWordOutlined style={{ color: blue.primary }} {...iconProps} />
      );
    case 'txt':
      return <FileTextOutlined {...iconProps} />;
    case 'jpg':
    case 'jpeg':
      return <FileJpgOutlined {...iconProps} />;
    case 'png':
    case 'svg':
    case 'webp':
      return <FileImageOutlined {...iconProps} />;
    case 'gif':
      return <FileGifOutlined {...iconProps} />;
    case 'xls':
    case 'xlsx':
      return (
        <FileExcelOutlined style={{ color: green.primary }} {...iconProps} />
      );
    case 'zip':
    case '7z':
    case 'rar':
      return <FileZipOutlined {...iconProps} />;
    default:
      return <FileOutlined {...iconProps} />;
  }
}
