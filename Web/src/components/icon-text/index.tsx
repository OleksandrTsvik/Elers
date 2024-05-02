import Icon from '@ant-design/icons';
import { Space } from 'antd';

interface Props {
  icon: React.FC;
  text: string;
}

export default function IconText({ icon, text }: Props) {
  return (
    <Space>
      <Icon component={icon} />
      {text}
    </Space>
  );
}
