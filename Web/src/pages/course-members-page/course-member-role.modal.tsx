import { FormInstance, Modal } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import CourseMemberRoleForm from './course-member-role.form';
import {
  closeRoleModal,
  selectCourseMembersState,
} from './course-members.slice';
import { useChangeCourseMemberRoleMutation } from '../../api/course-members.mutations.api';
import { useAppDispatch, useAppSelector } from '../../hooks/redux-hooks';

export interface CourseMemberRoleFormValues {
  courseRoleId?: string;
}

interface Props {
  courseId?: string;
}

export default function CourseMemberRoleModal({ courseId }: Props) {
  const { t } = useTranslation();
  const appDispatch = useAppDispatch();

  const [formInstance, setFormInstance] =
    useState<FormInstance<CourseMemberRoleFormValues>>();

  const { openMemberRoleModal, currentCourseMember } = useAppSelector(
    selectCourseMembersState,
  );

  const [changeCourseMemberRole, { isLoading, error }] =
    useChangeCourseMemberRoleMutation();

  const handleCloseModal = () => {
    appDispatch(closeRoleModal());
  };

  const handleSubmit = async ({ courseRoleId }: CourseMemberRoleFormValues) => {
    if (!currentCourseMember) {
      return;
    }

    await changeCourseMemberRole({
      memberId: currentCourseMember.id,
      courseRoleId,
    })
      .unwrap()
      .then(() => handleCloseModal());
  };

  return (
    <Modal
      destroyOnClose
      open={openMemberRoleModal}
      confirmLoading={isLoading}
      title={t('course_members_page.change_role')}
      okText={t('actions.save_changes')}
      onOk={formInstance?.submit}
      onCancel={handleCloseModal}
    >
      <CourseMemberRoleForm
        courseId={courseId}
        initialValues={{ courseRoleId: currentCourseMember?.courseRole?.id }}
        error={error}
        onSubmit={handleSubmit}
        onFormInstanceReady={setFormInstance}
      />
    </Modal>
  );
}
