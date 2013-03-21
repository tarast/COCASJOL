﻿/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var DesktopX = {
    alignPanels: function () {
        pnlSample.getEl().alignTo(Ext.getBody(), "tr", [-405, 5], false)
    },

    createDynamicWindow: function (app, ico, title, url, width, height) {
        var hostName = window.location.protocol + "//" + window.location.host;

        width = width == null ? 640 : width;
        height = height == null ? 480 : height;

        var desk = app.getDesktop();

        var w = desk.getWindow(title + '-win');
        if (!w) {
            var windowId = title + '-win';
            w = desk.createWindow({
                id: windowId,
                iconCls: 'icon-' + ico,
                title: title,
                width: width,
                height: height,
                maximizable: true,
                minimizable: true,
                closeAction: 'close',
                shadow: false,
                shim: false,
                animCollapse: false,
                constrainHeader: true,
                layout: 'fit',
                autoLoad: {
                    url: url,
                    mode: "iframe",
                    showMask: true
                },
                tools: [{
                    id: 'search',
                    qtip: 'Abrir en Ventana Propia',
                    handler: function (event, toolEl, panel) {
                        window.open(url, "_blank");
                    }
                }]
            });
            w.center();
        }
        w.show();
    },

    cascadeWindows: function (app) {
        app.getDesktop().cascade();
    },

    tileWindows: function (app) {
        app.getDesktop().tile();
    },

    checkerboardWindows: function (app) {
        var desk = app.getDesktop();

        var availWidth = desk.getWinWidth();
        var availHeight = desk.getWinHeight();

        var x = 0, y = 0;
        var lastx = 0, lasty = 0;

        var square = 400;

        desk.getManager().each(function (win) {
            if (win.isVisible()) {
                win.setWidth(square - 10);
                win.setHeight(square - 10);

                win.setPosition(x, y);
                x += square;

                if (x + square > availWidth) {
                    x = lastx;
                    y += square;

                    if (y > availHeight) {
                        lastx += 20;
                        lasty += 20;
                        x = lastx;
                        y = lasty;
                    }
                }
            }
        }, this);
    },

    tileFitWindows: function (app, horizontal) {
        var desk = app.getDesktop();
        var availWidth = desk.getWinWidth();
        var availHeight = desk.getWinHeight();

        var x = 0, y = 0;

        var snapCount = 0;

        desk.getManager().each(function (win) {
            if (win.isVisible()) {
                snapCount++;
            }
        }, this);

        var snapSize = parseInt(availWidth / snapCount);

        if (!horizontal)
            snapSize = parseInt(availHeight / snapCount);

        if (snapSize > 0) {
            desk.getManager().each(function (win) {
                if (win.isVisible()) {
                    if (horizontal) {
                        win.setWidth(snapSize);
                        win.setHeight(availHeight);
                    } else {
                        win.setWidth(availWidth);
                        win.setHeight(snapSize);
                    }

                    win.setPosition(x, y);

                    if (horizontal)
                        x += snapSize;
                    else
                        y += snapSize;
                }
            }, this);
        }
    },

    closeAllWindows: function (app) {
        Ext.Msg.confirm('Cerrar Ventanas', 'Todo el trabajo sin guardar en cada una de las ventanas se perdera. Seguro desea cerrar todas las ventanas?', function (btn, text) {
            if (btn == 'yes') {
                var desk = app.getDesktop();
                desk.getManager().each(function (win) {
                    var w = desk.getWindow(win.title + '-win');
                    if (w)
                        w.close();
                });
            }
        });
    },

    minimizeAllWindows: function (app) {
        var desk = app.getDesktop();
        desk.getManager().each(function (win) {
            var w = desk.getWindow(win.title + '-win');
            if (w && w.isVisible())
                win.minimize();
        });
    },

    showAllWindows: function (app) {
        var desk = app.getDesktop();
        desk.getManager().each(function (win) {
            var w = desk.getWindow(win.title + '-win');
            if (w && w.minimized)
                win.show();
        });
    },

    initialCheckForNotification: function () {
        Ext.net.DirectMethods.InitialCheckForNotifications();
    },

    markAsReadNotification: function (notificacion_id) {
        Ext.net.DirectMethods.MarkAsReadNotification(notificacion_id);
    },

    deleteReadNotifications: function () {
        if (dsReport.getCount() == 0)
            return;
        Ext.Msg.confirm('Eliminar Notificaciones Leidas', 'Seguro desea eliminar las notificaciones leidas?', function (btn, text) {
            if (btn == 'yes') {
                Ext.net.DirectMethods.DeleteReadNotifications();
            }
        });
    }
};

var WindowX = {
    usuarios: function (app) {
        DesktopX.createDynamicWindow(app, 'user', 'Usuarios', 'Seguridad/Usuarios.aspx');
    },

    roles: function (app) {
        DesktopX.createDynamicWindow(app, 'cog', 'Roles', 'Seguridad/Roles.aspx');
    },

    plantillasNotificaciones: function (app) {
        DesktopX.createDynamicWindow(app, 'plantillasNotificaciones16', 'Plantillas de Notificaciones', 'Utiles/PlantillasDeNotificaciones.aspx', 1000, 600);
    },

    socios: function (app) {
        DesktopX.createDynamicWindow(app, 'group', 'Socios', 'Socios/Socios.aspx', 800, 600);
    },

    estadosNotasDePeso: function (app) {
        DesktopX.createDynamicWindow(app, 'pagego', 'Estados de Notas de Peso', 'Inventario/Ingresos/EstadosNotaDePeso.aspx');
    },

    notasDePesoEnPesaje: function (app) {
        DesktopX.createDynamicWindow(app, 'pagewhiteput', 'Notas de Peso en Area de Pesaje', 'Inventario/Ingresos/NotasDePesoEnPesaje.aspx', 1000, 600);
    },

    notasDePesoEnCatacion: function (app) {
        DesktopX.createDynamicWindow(app, 'pagewhitecup', 'Notas de Peso en Area de Catación', 'Inventario/Ingresos/NotasDePesoEnCatacion.aspx', 1000, 600);
    },

    notasDePeso: function (app) {
        DesktopX.createDynamicWindow(app, 'pagewhiteoffice', 'Notas de Peso', 'Inventario/Ingresos/NotasDePesoEnAdministracion.aspx', 1000, 600);
    },

    inventarioDeCafePorSocio: function (app) {
        DesktopX.createDynamicWindow(app, 'bricks', 'Inventario de Café por Socio', 'Inventario/InventarioDeCafePorSocio.aspx');
    },

    hojasDeLiquidacion: function (app) {
        DesktopX.createDynamicWindow(app, 'script', 'Hojas de Liquidación', 'Inventario/Salidas/HojasDeLiquidacion.aspx', 1000, 600);
    },

    ventasDeInventarioDeCafe: function (app) {
        DesktopX.createDynamicWindow(app, 'cart_full', 'Ventas de Liquidación', 'Inventario/Salidas/VentasDeInventarioDeCafe.aspx');
    },

    movimientosDeInventarioDeCafe: function (app) {
        DesktopX.createDynamicWindow(app, 'report', 'Reporte de Movimientos de Inventario de Café', 'Reportes/MovimientosDeInventarioDeCafe.aspx', 1000, 600);
    },

    aportacionesPorSocio: function (app) {
        DesktopX.createDynamicWindow(app, 'aportacionesPorSocio16', 'Aportaciones por Socio', 'Aportaciones/AportacionesPorSocio.aspx', 1000, 600);
    },

    retiroDeAportaciones: function (app) {
        DesktopX.createDynamicWindow(app, 'retiroAportaciones16', 'Retiro de Aportaciones', 'Aportaciones/RetiroDeAportaciones.aspx', 1000, 600);
    },

    solicitudesDePrestamo: function (app) {
        DesktopX.createDynamicWindow(app, 'pagewhitetext', 'Solicitudes de Prestamo', 'Prestamos/SolicitudesDePrestamos.aspx');
    },

    prestamos: function (app) {
        DesktopX.createDynamicWindow(app, 'prestamos16', 'Prestamos', 'Prestamos/Prestamos.aspx');
    },

    clasificacionesDeCafe: function (app) {
        DesktopX.createDynamicWindow(app, 'cup', 'Clasificaciones de Café', 'Inventario/ClasificacionesDeCafe.aspx');
    },

    variablesDeEntorno: function (app) {
        DesktopX.createDynamicWindow(app, 'database', 'Variables de Entorno', 'Entorno/VariablesDeEntorno.aspx');
    },

    settings: function () {
        SettingsWin.show();
    },

    about: function () {
        AboutWin.show();
    }
};

var ShorcutClickHandler = function (app, id) {
    var d = app.getDesktop();

    if (id == 'scUsuarios') {
        WindowX.usuarios(app);
    } else if (id == 'scRoles') {
        WindowX.roles(app);
    } else if (id == 'scPlantillasNotificaciones') {
        WindowX.plantillasNotificaciones(app);
    } else if (id == 'scSocios') {
        WindowX.socios(app);
    } else if (id == 'scEstadosNotasDePeso') {
        WindowX.estadosNotasDePeso(app);
    } else if (id == 'scNotasDePesoEnPesaje') {
        WindowX.notasDePesoEnPesaje(app);
    } else if (id == 'scNotasDePesoEnCatacion') {
        WindowX.notasDePesoEnCatacion(app);
    } else if (id == 'scNotasDePeso') {
        WindowX.notasDePeso(app);
    } else if (id == 'scInventarioDeCafePorSocio') {
        WindowX.inventarioDeCafePorSocio(app);
    } else if (id == 'scHojasDeLiquidacion') {
        WindowX.hojasDeLiquidacion(app);
    } else if (id == 'scVentasDeInventarioDeCafe') {
        WindowX.ventasDeInventarioDeCafe(app);
    } else if (id == 'scMovimientosDeInventarioDeCafe') {
        WindowX.movimientosDeInventarioDeCafe(app);
    } else if (id == 'scAportacionesPorSocio') {
        WindowX.aportacionesPorSocio(app);
    } else if (id == 'scRetiroDeAportaciones') {
        WindowX.retiroDeAportaciones(app);
    } else if (id == 'scSolicitudesDePrestamo') {
        WindowX.solicitudesDePrestamo(app);
    } else if (id == 'scPrestamos') {
        WindowX.prestamos(app);
    } else if (id == 'scClasificacionesDeCafe') {
        WindowX.clasificacionesDeCafe(app);
    } else if (id == 'scVariablesDeEntorno') {
        WindowX.variablesDeEntorno(app);
    }
};