import { red } from '@ant-design/colors';
import { DeleteFilled } from '@ant-design/icons';

export default function DeleteIcon(props: typeof DeleteFilled.defaultProps) {
  return <DeleteFilled style={{ color: red.primary }} {...props} />;
}
