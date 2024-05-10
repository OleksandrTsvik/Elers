import { lime } from '@ant-design/colors';
import { NumberOutlined } from '@ant-design/icons';
import { GetProps } from 'antd';

type Props = GetProps<typeof NumberOutlined>;

export default function CountIcon(props: Props) {
  return <NumberOutlined style={{ color: lime.primary }} {...props} />;
}
