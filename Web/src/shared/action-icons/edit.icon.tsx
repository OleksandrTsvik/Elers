import { blue } from '@ant-design/colors';
import { EditFilled } from '@ant-design/icons';

export default function EditIcon(props: typeof EditFilled.defaultProps) {
  return <EditFilled style={{ color: blue.primary }} {...props} />;
}
