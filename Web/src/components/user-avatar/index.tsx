import { UserOutlined } from '@ant-design/icons';
import { Avatar, GetProps } from 'antd';

type AvatarProps = GetProps<typeof Avatar>;

interface Props extends AvatarProps {
  url?: string;
}

export default function UserAvatar({ url, ...props }: Props) {
  return url ? (
    <Avatar src={url} {...props} />
  ) : (
    <Avatar icon={<UserOutlined />} {...props} />
  );
}
