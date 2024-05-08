import { ButtonProps, FormInstance, Modal } from 'antd';
import { useState } from 'react';

import TabNameForm, { TabNameFormValues } from '../forms/tab-name.form';

interface Props {
  isOpen: boolean;
  initialValues: TabNameFormValues;
  textTitle: string;
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  okButtonProps?: ButtonProps;
  onClose: () => void;
  onSubmit: (values: TabNameFormValues) => Promise<void> | void;
}

export default function TabNameModal({
  isOpen,
  initialValues,
  textTitle,
  textOnSubmitButton,
  isLoading,
  error,
  okButtonProps,
  onClose,
  onSubmit,
}: Props) {
  const [formInstance, setFormInstance] =
    useState<FormInstance<TabNameFormValues>>();

  const handleSubmit = async (values: TabNameFormValues) => {
    await Promise.resolve(onSubmit(values)).then(() => onClose());
  };

  return (
    <Modal
      destroyOnClose
      open={isOpen}
      confirmLoading={isLoading}
      title={textTitle}
      okText={textOnSubmitButton}
      okButtonProps={okButtonProps}
      onOk={formInstance?.submit}
      onCancel={onClose}
    >
      <TabNameForm
        initialValues={initialValues}
        error={error}
        onSubmit={handleSubmit}
        onFormInstanceReady={setFormInstance}
      />
    </Modal>
  );
}
