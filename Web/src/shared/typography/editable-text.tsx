import { CheckOutlined, CloseOutlined } from '@ant-design/icons';
import { Button, Form, GetProps } from 'antd';
import { Rule } from 'antd/es/form';
import { InputProps, TextAreaProps } from 'antd/es/input';
import { ReactNode, useState } from 'react';
import { useTranslation } from 'react-i18next';

import EditableTextInput from './editable-text.input';
import { ErrorAlert } from '../../components';

import styles from './typography.module.scss';

interface FormValues {
  value: string;
}

type FormProps = GetProps<typeof Form<FormValues>>;

interface CommonProps {
  text: string | undefined;
  loading?: boolean;
  changeText?: string;
  textRules?: Rule[];
  error?: unknown;
  formProps?: FormProps;
  children: ReactNode;
  onChange: (value: string) => Promise<void> | void;
}

type ConditionalProps =
  | { type: 'input'; inputProps?: InputProps }
  | { type: 'textarea'; inputProps?: TextAreaProps };

type Props = CommonProps & ConditionalProps;

export default function EditableText({
  text,
  loading,
  changeText,
  textRules,
  error,
  formProps,
  children,
  onChange,
  ...props
}: Props) {
  const { t } = useTranslation();

  const [isEditing, setIsEditing] = useState(false);
  const [form] = Form.useForm<FormValues>();

  const handleCancel = () => {
    setIsEditing(false);
    form.resetFields();
  };

  const handleSave = () => {
    form.submit();
  };

  const handleSubmit = async ({ value }: FormValues) => {
    if (text === value) {
      setIsEditing(false);
      return;
    }

    await Promise.resolve(onChange(value)).then(() => {
      setIsEditing(false);
    });
  };

  if (!isEditing) {
    return (
      <>
        {children}
        <Button
          className={styles.editableText_changeButton}
          type="link"
          onClick={() => setIsEditing(true)}
        >
          {changeText || t('actions.change')}
        </Button>
      </>
    );
  }

  return (
    <>
      {error && <ErrorAlert error={error} style={{ marginBottom: '0.5em' }} />}
      <Form
        {...formProps}
        form={form}
        initialValues={{ value: text || '' }}
        onFinish={handleSubmit}
      >
        <Form.Item hasFeedback name="value" rules={textRules}>
          <EditableTextInput inputType={props.type} {...props.inputProps} />
        </Form.Item>
      </Form>
      <div className={styles.editableText_actionButtons}>
        <Button
          danger
          type="primary"
          icon={<CloseOutlined />}
          onClick={handleCancel}
        />
        <Button
          type="primary"
          loading={loading}
          icon={<CheckOutlined />}
          onClick={handleSave}
        />
      </div>
    </>
  );
}
