import { FormInstance, Modal } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import CourseRoleForm, { CourseRoleFormValues } from './course-role.form';
import { CourseRolesModalMode } from './course-roles.modal-mode.enum';
import {
  useCreateCourseRoleMutation,
  useUpdateCourseRoleMutation,
} from '../../api/course-roles.api';
import { CoursePermissionListItem } from '../../models/course-permission.interface';
import { CourseRoleListItem } from '../../models/course-role.interface';

interface Props {
  courseId: string;
  modalMode?: CourseRolesModalMode;
  permissions: CoursePermissionListItem[];
  currentCourseRole?: CourseRoleListItem;
  onCloseModal: () => void;
}

export default function CourseRolesModals({
  courseId,
  modalMode,
  permissions,
  currentCourseRole,
  onCloseModal,
}: Props) {
  const { t } = useTranslation();

  const [formInstance, setFormInstance] =
    useState<FormInstance<CourseRoleFormValues>>();

  const [createCourseRole, { isLoading: isLoadingCreate, error: errorCreate }] =
    useCreateCourseRoleMutation();

  const [updateCourseRole, { isLoading: isLoadingUpdate, error: errorUpdate }] =
    useUpdateCourseRoleMutation();

  const handleCreateSubmit = async (values: CourseRoleFormValues) => {
    await createCourseRole({ courseId, ...values })
      .unwrap()
      .then(() => onCloseModal());
  };

  const handleUpdateSubmit = async (values: CourseRoleFormValues) => {
    await updateCourseRole({
      roleId: currentCourseRole?.id ?? '',
      ...values,
    })
      .unwrap()
      .then(() => onCloseModal());
  };

  return (
    <>
      <Modal
        destroyOnClose
        open={modalMode === CourseRolesModalMode.Create}
        confirmLoading={isLoadingCreate}
        title={t('course_roles_page.create')}
        okText={t('actions.add')}
        onOk={formInstance?.submit}
        onCancel={onCloseModal}
      >
        <CourseRoleForm
          permissions={permissions}
          error={errorCreate}
          onSubmit={handleCreateSubmit}
          onFormInstanceReady={setFormInstance}
        />
      </Modal>
      <Modal
        destroyOnClose
        open={modalMode === CourseRolesModalMode.Edit}
        confirmLoading={isLoadingUpdate}
        title={t('course_roles_page.update')}
        okText={t('actions.save_changes')}
        onOk={formInstance?.submit}
        onCancel={onCloseModal}
      >
        <CourseRoleForm
          initialValues={
            currentCourseRole
              ? {
                  name: currentCourseRole.name,
                  permissionIds: currentCourseRole.coursePermissions.map(
                    (item) => item.id,
                  ),
                }
              : undefined
          }
          permissions={permissions}
          error={errorUpdate}
          onSubmit={handleUpdateSubmit}
          onFormInstanceReady={setFormInstance}
        />
      </Modal>
    </>
  );
}
