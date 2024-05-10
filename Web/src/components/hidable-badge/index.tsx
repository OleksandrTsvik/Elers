import { Badge, GetProps } from 'antd';

type Props = { show: boolean } & GetProps<typeof Badge>;

export default function HidableBadge({ show, children, ...props }: Props) {
  if (!show) {
    return children;
  }

  return (
    <Badge showZero {...props}>
      {children}
    </Badge>
  );
}
