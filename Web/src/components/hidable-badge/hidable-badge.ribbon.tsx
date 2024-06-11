import { Badge, GetProps } from 'antd';

type Props = { show: boolean } & GetProps<typeof Badge.Ribbon>;

export default function HidableBadgeRibbon({
  show,
  children,
  ...props
}: Props) {
  if (!show) {
    return children;
  }

  return <Badge.Ribbon {...props}>{children}</Badge.Ribbon>;
}
