import { LoginOutlined } from '@ant-design/icons';
import { Button } from 'antd';
import { Link } from 'react-router-dom';

export default function LoginButton() {
  return (
    <Link to="/login">
      <Button type="text" icon={<LoginOutlined />} />
    </Link>
  );
}
