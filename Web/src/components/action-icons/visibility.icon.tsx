import { EyeInvisibleOutlined, EyeOutlined } from '@ant-design/icons';
import { GetProps } from 'antd';

type VisibleIconProps = GetProps<typeof EyeOutlined>;
type InvisibleIconProps = GetProps<typeof EyeInvisibleOutlined>;

type VisibleProps = { visibility: true } & InvisibleIconProps;
type InvisibleProps = { visibility: false } & VisibleIconProps;

type Props = VisibleProps | InvisibleProps;

export default function VisibilityIcon({ visibility, ...props }: Props) {
  return visibility ? (
    <EyeOutlined {...(props as VisibleIconProps)} />
  ) : (
    <EyeInvisibleOutlined {...(props as InvisibleIconProps)} />
  );
}
