import { purple } from '@ant-design/colors';
import { FontColorsOutlined } from '@ant-design/icons';
import { GetProps } from 'antd';

type Props = GetProps<typeof FontColorsOutlined>;

export default function ColorIcon(props: Props) {
  return <FontColorsOutlined style={{ color: purple.primary }} {...props} />;
}
