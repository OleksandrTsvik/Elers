import { Input } from 'antd';
import { InputProps, TextAreaProps } from 'antd/es/input';

import styles from './editable-text.module.scss';

interface CommonProps {
  inputType: 'input' | 'textarea';
}

type ConditionalProps = InputProps | TextAreaProps;

type Props = CommonProps & ConditionalProps;

export default function EditableTextInput({ inputType, ...props }: Props) {
  switch (inputType) {
    case 'textarea':
      return (
        <Input.TextArea
          className={styles.textarea}
          showCount
          autoSize={{ minRows: 2 }}
          {...(props as TextAreaProps)}
        />
      );
    default:
      return <Input showCount {...(props as InputProps)} />;
  }
}
