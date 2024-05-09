import { purple } from '@ant-design/colors';
import { FontColorsOutlined } from '@ant-design/icons';
import { GetProps } from 'react-redux';

type Props = GetProps<typeof FontColorsOutlined>;

export default function ChangeColorIcon(props: Props) {
  return <FontColorsOutlined style={{ color: purple.primary }} {...props} />;
}
